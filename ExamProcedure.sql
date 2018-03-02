USE [master]
GO

/****** Object:  Database [PremierDataWarehouse]    Script Date: 02/24/18 8:47:24 AM ******/
CREATE DATABASE [PremierDataWarehouse]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PremierDataWarehouse', FILENAME = N'N:\SQLData\PremierDataWarehouse.mdf' , SIZE = 2412544KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'PremierDataWarehouse_log', FILENAME = N'N:\SQLData\PremierDataWarehouse_log.ldf' , SIZE = 13217792KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [PremierDataWarehouse] SET COMPATIBILITY_LEVEL = 120
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PremierDataWarehouse].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [PremierDataWarehouse] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [PremierDataWarehouse] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [PremierDataWarehouse] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [PremierDataWarehouse] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [PremierDataWarehouse] SET ARITHABORT OFF 
GO

ALTER DATABASE [PremierDataWarehouse] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [PremierDataWarehouse] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [PremierDataWarehouse] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [PremierDataWarehouse] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [PremierDataWarehouse] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [PremierDataWarehouse] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [PremierDataWarehouse] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [PremierDataWarehouse] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [PremierDataWarehouse] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [PremierDataWarehouse] SET  DISABLE_BROKER 
GO

ALTER DATABASE [PremierDataWarehouse] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [PremierDataWarehouse] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [PremierDataWarehouse] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [PremierDataWarehouse] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [PremierDataWarehouse] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [PremierDataWarehouse] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [PremierDataWarehouse] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [PremierDataWarehouse] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [PremierDataWarehouse] SET  MULTI_USER 
GO

ALTER DATABASE [PremierDataWarehouse] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [PremierDataWarehouse] SET DB_CHAINING OFF 
GO

ALTER DATABASE [PremierDataWarehouse] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [PremierDataWarehouse] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [PremierDataWarehouse] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [PremierDataWarehouse] SET  READ_WRITE 
GO




USE [PremierDataWarehouse]
GO

/****** Object:  StoredProcedure [dbo].[xp_backend_login]    Script Date: 02/24/18 8:48:44 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[xp_backend_login](@username varchar(50), @password varchar(100))
as
begin
	declare @tmp varchar(100), @status tinyint, @EmployeeISN int
	select @tmp=empPassword, @status=empStatus, @EmployeeISN=EmployeeISN from Employee
			where empUserName=@username
	if(@tmp is null) return -4
	if(@status=0) return -3
	if(@tmp<>@password) return -2
	return @employeeisn
end

GO

USE [PremierDataWarehouse]
GO

/****** Object:  StoredProcedure [dbo].[xp_calculatehistory_getpage]    Script Date: 02/24/18 8:49:27 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[xp_calculatehistory_getpage](@condition nvarchar(3000)='', @nItemPage int=20, @curpage int=1, @NoRec int=null, 
	@SessionID varchar(50)=null, @sortBy varchar(100)='HistoryISN', @sortDirect varchar(10)='DESC')
as
begin
	declare @tableOrview varchar(100)
	set @tableOrview='Vw_CalculateHistory'

	declare @qry nvarchar(4000), @n int
	
	if(@condition<>'') set @condition= ' where ' + @condition
	
	if(@NoRec is null or  @NoRec<=0)
	begin
		create table #tmp(NoRec int)
		set @qry='insert into #tmp(NoRec) select count(*) from ' + @tableOrview + ' ' + @condition
		
		
		exec(@qry); select @norec=NoRec from #tmp
		drop table #tmp
	END
	
	declare @minISN int, @maxISN int
	set @minISN=(@curpage-1)*@nItemPage + 1
	set @maxISN=@curpage*@nItemPage
	
	set @qry='select a.* from
				(
				SELECT ROW_NUMBER() OVER (ORDER BY ' + @sortBy + ' ' + @sortDirect + ') as ROWID, a.*
				from ' + @tableOrview + ' a ' + @condition + '
				) as a
				where RowID between ' + cast(@minISN AS varchar) + ' and ' + cast(@maxISN AS varchar) + ' order by RowID' 
	
	print @qry
	exec (@qry)
	
	select @norec as NoOfRec
	return @norec
end

GO


USE [PremierDataWarehouse]
GO

/****** Object:  StoredProcedure [dbo].[xp_calculatehistory_ins]    Script Date: 02/24/18 8:49:48 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[xp_calculatehistory_ins](@PercentSalesRepresentative float, @NewLeadPerSalesRepresentativeDay float,
	@OpenerAppConversionRate float, @CallstoOpenerDay float, @WorkdaysWeek float, @NoOfSalesRepresentative float,
	@NoOfOpenerNeeded float, @TotalCallsWeek float, @DebtLoadDesc nvarchar(2000)=null, @CountISN int, @TotalPieces int, 
	@Details varchar(8000), @updatedBy int)
as
begin
	declare @HistoryISN int, @idoc int
	
	declare @tbDetail as table (SettingID int, Leads int, MailPercent float, PieceQty int)
	exec sp_xml_preparedocument @idoc OUTPUT, @Details
	Insert Into @tbDetail(SettingID, Leads, MailPercent, PieceQty)
	select SettingID, Leads, MailPercent, PieceQty
	from OPENXML(@idoc, '/Root/Detail') 
	WITH (SettingID int, Leads int, MailPercent float, PieceQty int)
	exec sp_xml_removedocument @idoc

	Insert Into CalculateHistory(PercentSalesRepresentative, NewLeadPerSalesRepresentativeDay, OpenerAppConversionRate, CallstoOpenerDay,
		WorkdaysWeek, NoOfSalesRepresentative, NoOfOpenerNeeded, TotalCallsWeek, DebtLoadDesc, CountISN, TotalPieces, updatedBy)
	Values(@PercentSalesRepresentative, @NewLeadPerSalesRepresentativeDay, @OpenerAppConversionRate, @CallstoOpenerDay, 
		@WorkdaysWeek, @NoOfSalesRepresentative, @NoOfOpenerNeeded, @TotalCallsWeek, @DebtLoadDesc, @CountISN, @TotalPieces, @updatedBy)
	set @HistoryISN=SCOPE_IDENTITY()

	Insert Into [dbo].[CalculateHistoryDetail](HistoryISN, SettingID, DebtLoad, LeadProjected, MinAmount, MaxAmount, Leads, MailPercent, PieceQty, updatedBy)
		select @HistoryISN, a.SettingID, b.DebtLoad, b.LeadProjected, b.MinAmount, b.MaxAmount, a.Leads, a.MailPercent, a.PieceQty, @updatedBy
		from @tbDetail a, RateSetting b
		where a.SettingID=b.SettingID

	return @HistoryISN
end

GO


USE [PremierDataWarehouse]
GO

/****** Object:  StoredProcedure [dbo].[xp_datacount_getpage]    Script Date: 02/24/18 8:50:33 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[xp_datacount_getpage](@condition nvarchar(3000)='', @nItemPage int=20, @curpage int=1, @NoRec int=null, 
	@SessionID varchar(50)=null, @sortBy varchar(100)='CountISN', @sortDirect varchar(10)='DESC')
as
begin
	declare @tableOrview varchar(100)
	set @tableOrview='Vw_DataCount'

	declare @qry nvarchar(4000), @n int
	
	if(@condition<>'') set @condition= ' where ' + @condition
	
	if(@NoRec is null or  @NoRec<=0)
	begin
		create table #tmp(NoRec int)
		set @qry='insert into #tmp(NoRec) select count(*) from ' + @tableOrview + ' ' + @condition
		
		
		exec(@qry); select @norec=NoRec from #tmp
		drop table #tmp
	END
	
	declare @minISN int, @maxISN int
	set @minISN=(@curpage-1)*@nItemPage + 1
	set @maxISN=@curpage*@nItemPage
	
	set @qry='select a.* from
				(
				SELECT ROW_NUMBER() OVER (ORDER BY ' + @sortBy + ' ' + @sortDirect + ') as ROWID, a.*
				from ' + @tableOrview + ' a ' + @condition + '
				) as a
				where RowID between ' + cast(@minISN AS varchar) + ' and ' + cast(@maxISN AS varchar) + ' order by RowID' 
	
	print @qry
	exec (@qry)
	
	select @norec as NoOfRec
	return @norec
end

GO


USE [PremierDataWarehouse]
GO

/****** Object:  StoredProcedure [dbo].[xp_datacount_getaverage]    Script Date: 02/24/18 8:50:29 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Proc [dbo].[xp_datacount_getaverage](@CountISN int)
as
begin
	declare @TotalQty float, @TotalDebt1Qty int, @TotalDebt2Qty int, @Debt1Percent float, @Debt2Percent float
		
	select @TotalDebt1Qty=sum(Debt1Qty), @TotalDebt2Qty=sum(Debt2Qty)
	from DataCountProviderDetailFile 
	where CountISN=@CountISN

	set @TotalDebt1Qty=isnull(@TotalDebt1Qty, 0)
	set @TotalDebt2Qty=isnull(@TotalDebt2Qty, 0)

	set @TotalQty = @TotalDebt1Qty + @TotalDebt2Qty

	if(@TotalQty=0) return -2

	set @Debt1Percent = @TotalDebt1Qty/@TotalQty
	set @Debt2Percent = @TotalDebt2Qty/@TotalQty

	select @TotalDebt1Qty as TotalDebt1Qty, @TotalDebt2Qty as TotalDebt2Qty, @TotalQty as TotalQty,
		@Debt1Percent as Debt1Percent, @Debt2Percent as Debt2Percent
	return 0;
end


GO


USE [PremierDataWarehouse]
GO

/****** Object:  StoredProcedure [dbo].[xp_datacount_muldel]    Script Date: 02/24/18 8:51:49 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[xp_datacount_muldel](@lstISNs varchar(8000))
as
begin
	declare @ObjISN int, @ISN int, @Name nvarchar(1000)
	
	Create Table #temp(ISN int, [Name] nvarchar(1000), Code varchar(200))
	Create Table #tempISN(ISN int  identity, Info int)
	exec xp_string_split @lstISNs, '#tempISN'
	declare curdel cursor local for
		select ISN, convert(int,Info) from #tempISN order by ISN
	open curdel
	fetch next from curdel into @isn, @ObjISN
	while(@@fetch_status=0)
	begin
		
		if exists(select * from DataCount where CountISN=@ObjISN and TotalReceivedQty>0)
		begin
			select @Name=CountName from DataCount where CountISN=@ObjISN
			Insert Into #temp(ISN, [Name], Code) values(@ObjISN, @Name, -2)
			break
		end

		delete from DataCount where CountISN=@ObjISN
		
		fetch next from curdel into @isn, @ObjISN
	end
	close curdel
	deallocate curdel
	
	select * from #temp;
	
	drop table #tempISN
	drop table #temp
	
	return 0
end

GO


USE [PremierDataWarehouse]
GO

/****** Object:  StoredProcedure [dbo].[xp_datacount_insupd]    Script Date: 02/24/18 8:51:45 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[xp_datacount_insupd](@CountISN int=null, @CountName nvarchar(100)=null, @CriteriaFiles varchar(500)=null,
	@ProviderTotalRecords int=null, @ProviderFiles varchar(500)=null, @ProviderFileContent nvarchar(max)=null, 
	@Description nvarchar(500)=null, @OrderFileName varchar(100)=null, @DataReceivedFiles varchar(500)=null, @Status tinyint=null, @updatedBy int)
as
begin
	set @CountISN=isnull(@CountISN, 0)
	if(@CountISN<=0)
	begin
		Insert Into DataCount(CountName, CriteriaFiles, ProviderTotalRecords, ProviderFiles, [Description], addedBy, updatedBy)
			Values(@CountName, @CriteriaFiles, @ProviderTotalRecords, @ProviderFiles, @Description, @updatedBy, @updatedBy)

		set @CountISN=SCOPE_IDENTITY()
	end
	else
	begin
		update DataCount
		set CountName=isnull(@CountName, CountName), 
			CriteriaFiles=isnull(@CriteriaFiles, CriteriaFiles), 
			ProviderTotalRecords=isnull(@ProviderTotalRecords, ProviderTotalRecords),
			ProviderFiles=isnull(@ProviderFiles, ProviderFiles), 
			[Description]=isnull(@Description, [Description]), 
			OrderFileName=isnull(@OrderFileName, OrderFileName),
			DataReceivedFiles=isnull(@DataReceivedFiles, DataReceivedFiles),
			[Status]=isnull(@Status, [Status]),
			updatedDate=getdate(), updatedBy=@updatedBy
		where CountISN=@CountISN
	end

	if(@ProviderFileContent is not null and @ProviderFileContent<>'')
	begin
		declare @idoc int
		declare @tbFileContent as table([State] varchar(50), [Debt1Qty] int, [Debt1Percent] float, [Debt2Qty] int, [Debt2Percent] float,
					[Debt3Qty] int, [Debt3Percent] float, TotalQty int)

		exec sp_xml_preparedocument @idoc OUTPUT, @ProviderFileContent
		Insert Into @tbFileContent([State], Debt1Qty, Debt1Percent, Debt2Qty, Debt2Percent, Debt3Qty, Debt3Percent, TotalQty)
		select [State], Debt1Qty, Debt1Percent, Debt2Qty, Debt2Percent, Debt3Qty, Debt3Percent, TotalQty
		from OPENXML(@idoc, '/Root/Info') 
		WITH (State varchar(50), Debt1Qty int, Debt1Percent float, Debt2Qty int, Debt2Percent float, Debt3Qty int, Debt3Percent float, TotalQty int)
		exec sp_xml_removedocument @idoc

		delete from DataCountProviderDetailFile where CountISN=@CountISN

		Insert Into DataCountProviderDetailFile(CountISN, [State], Debt1Qty, Debt1Percent, Debt2Qty, Debt2Percent, Debt3Qty, Debt3Percent, TotalQty, updatedBy)
			select @CountISN, [State], Debt1Qty, Debt1Percent, Debt2Qty, Debt2Percent, Debt3Qty, Debt3Percent, TotalQty, @updatedBy 
			from @tbFileContent
			where Debt1Qty>0 and Debt2Qty>0

		select @ProviderTotalRecords=sum( isnull(Debt1Qty,0) + isnull(Debt2Qty,0) + isnull(Debt3Qty,0) )
		from DataCountProviderDetailFile
		where CountISN=@CountISN

		update DataCount set ProviderTotalRecords=@ProviderTotalRecords where CountISN=@CountISN
	end

	return @CountISN
end

GO


USE [PremierDataWarehouse]
GO

/****** Object:  StoredProcedure [dbo].[xp_dataexport_del]    Script Date: 02/24/18 8:52:52 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[xp_dataexport_del](@ExportISN int)
as
begin
	if exists(select * from DataExport where ExportISN=@ExportISN and PieceQty is not null and PieceQty>0)
		return -2

	if not exists(select * from DataExport where ExportISN=@ExportISN and ExportDate>dbo.DateOnly(getdate()-7))
		return -3

	update Member 
	set LastExportDate=null
	where MemberISN in(select MemberISN from MemberOfExport where ExportISN=@ExportISN)

	delete from MemberOfExport where ExportISN=@ExportISN

	delete from DataExport where ExportISN=@ExportISN

	return 0
end

GO


USE [PremierDataWarehouse]
GO

/****** Object:  StoredProcedure [dbo].[xp_dataexport_getpage]    Script Date: 02/24/18 9:10:28 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[xp_dataexport_getpage](@condition nvarchar(3000)='', @nItemPage int=20, @curpage int=1, @NoRec int=null, 
	@SessionID varchar(50)=null, @sortBy varchar(100)='ExportISN', @sortDirect varchar(10)='DESC')
as
begin
	declare @tableOrview varchar(100)
	set @tableOrview='Vw_DataExport'

	declare @qry nvarchar(4000), @n int
	
	if(@condition<>'') set @condition= ' where ' + @condition
	
	if(@NoRec is null or  @NoRec<=0)
	begin
		create table #tmp(NoRec int)
		set @qry='insert into #tmp(NoRec) select count(*) from ' + @tableOrview + ' ' + @condition
		
		
		exec(@qry); select @norec=NoRec from #tmp
		drop table #tmp
	END
	
	declare @minISN int, @maxISN int
	set @minISN=(@curpage-1)*@nItemPage + 1
	set @maxISN=@curpage*@nItemPage
	
	set @qry='select a.* from
				(
				SELECT ROW_NUMBER() OVER (ORDER BY ' + @sortBy + ' ' + @sortDirect + ') as ROWID, a.*
				from ' + @tableOrview + ' a ' + @condition + '
				) as a
				where RowID between ' + cast(@minISN AS varchar) + ' and ' + cast(@maxISN AS varchar) + ' order by RowID' 
	
	print @qry
	exec (@qry)
	
	select @norec as NoOfRec
	return @norec
end

GO


USE [PremierDataWarehouse]
GO

/****** Object:  StoredProcedure [dbo].[xp_dataexport_upd2]    Script Date: 02/24/18 9:10:31 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[xp_dataexport_upd2](@ExportISN int, @CampaignPrefix nvarchar(20)=null, @CodeLength int=null, @CodeStart int=null, 
	@IVRName nvarchar(50)=null, @DDSessionID varchar(100)=null, @JobNumber varchar(50)=null, @MailingDate datetime='1/1/1900', @updatedBy int)
as
begin
	update DataExport
	set IVRName=isnull(@IVRName, IVRName),
		DDSessionID=isnull(@DDSessionID, DDSessionID),
		JobNumber=isnull(@JobNumber, JobNumber),
		MailingDate= (case when @MailingDate='1/1/1900' then MailingDate else @MailingDate end),
		CampaignPrefix=isnull(@CampaignPrefix, CampaignPrefix),
		CodeLength=isnull(@CodeLength, CodeLength),
		CodeStart=isnull(@CodeStart, CodeStart),
		updatedDate=getdate(), updatedBy=@updatedBy
	where ExportISN=@ExportISN

	return @ExportISN
end

GO

USE [PremierDataWarehouse]
GO

/****** Object:  StoredProcedure [dbo].[xp_dataorder_generate]    Script Date: 02/24/18 9:11:19 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[xp_dataorder_generate](@CountISN int, @Order1Qty int, @Order2Qty int, @updatedBy int)
as
begin
	declare @Debt1TotalQty int, @Debt2TotalQty int, @Percent float
	
	--if exists(select * from DataCount where CountISN=@CountISN and TotalReceivedQty>0)
	--	return -4

	declare @tmpProviderFile as table ([State] varchar(50), [Debt1Qty] int, [Debt2Qty] int, [Order1Qty] int default 0, [Order2Qty] int default 0,
		Debt1Percent float, Debt2Percent float)

	Insert Into @tmpProviderFile([State], [Debt1Qty], [Debt2Qty], Debt1Percent, Debt2Percent)
	select [State], [Debt1Qty], [Debt2Qty], Debt1Percent, Debt2Percent
	from DataCountProviderDetailFile 
	where CountISN=@CountISN

	select @Debt1TotalQty=sum([Debt1Qty]), @Debt2TotalQty=sum([Debt2Qty]) from @tmpProviderFile

	if(@Order1Qty<=0 and @Order2Qty<=0) return -3

	if(@Debt1TotalQty<@Order1Qty or @Debt2TotalQty<@Order2Qty) return -2

	declare @State varchar(20), @StoreQty int, @Qty int, @RemainTotalQty int, @NoRec int, @Count int

	-- Order1 Qty
	if(@Order1Qty>0)
	begin
		if(@Order1Qty=@Debt1TotalQty)
		begin
			update @tmpProviderFile set Order1Qty=Debt1Qty
		end
		else
		begin
			select @NoRec = count(*) from @tmpProviderFile where [Debt1Qty]>0
			set @Count=1
			set @RemainTotalQty=@Order1Qty

			declare cur1 cursor local for
				select [State], [Debt1Qty], Debt1Percent from @tmpProviderFile where Debt1Percent>0 order by Debt1Percent
			open cur1
			fetch next from cur1 into @State, @StoreQty, @Percent
			while(@@FETCH_STATUS=0)
			begin
				if(@Count<@NoRec)
				begin
					set @Qty = round(@Order1Qty*@Percent,0)
					
					if(@Qty>@StoreQty) set @Qty=@StoreQty

					if(@Qty>@RemainTotalQty) set @Qty=@RemainTotalQty

					update @tmpProviderFile set Order1Qty=@Qty where [State]=@State

					set @RemainTotalQty = @RemainTotalQty - @Qty
					
					if(@RemainTotalQty<=0)
					begin 
						print @RemainTotalQty
						break;
					end
				end
				else
				begin
					update @tmpProviderFile set Order1Qty=@RemainTotalQty where [State]=@State
				end

				set @Count=@Count+1
				fetch next from cur1 into @State, @StoreQty, @Percent
			end
			close cur1
			deallocate cur1
		end
	end

	-- Order2 Qty
	if(@Order2Qty>0)
	begin
		if(@Order2Qty=@Debt2TotalQty)
		begin
			update @tmpProviderFile set Order2Qty=Debt2Qty
		end
		else
		begin
			select @NoRec = count(*) from @tmpProviderFile where Debt2Qty>0
			set @Count=1
			set @RemainTotalQty=@Order2Qty

			declare cur1 cursor local for
				select [State], Debt2Qty, Debt2Percent from @tmpProviderFile where Debt2Percent>0 order by Debt2Percent
			open cur1
			fetch next from cur1 into @State, @StoreQty, @Percent
			while(@@FETCH_STATUS=0)
			begin
				if(@Count<@NoRec)
				begin
					set @Qty = round(@Order2Qty*@Percent,0)
					
					if(@Qty>@StoreQty) set @Qty=@StoreQty

					if(@Qty>@RemainTotalQty) set @Qty=@RemainTotalQty

					update @tmpProviderFile set Order2Qty=@Qty where [State]=@State

					set @RemainTotalQty = @RemainTotalQty - @Qty
					
					if(@RemainTotalQty<=0)
					begin 
						print @RemainTotalQty
						break;
					end
				end
				else
				begin
					update @tmpProviderFile set Order2Qty=@RemainTotalQty where [State]=@State
				end

				set @Count=@Count+1
				fetch next from cur1 into @State, @StoreQty, @Percent
			end
			close cur1
			deallocate cur1
		end
	end

	delete from DataOrderDetail where CountISN=@CountISN

	Insert Into DataOrderDetail(CountISN, [State], [Debt1Qty], [Debt2Qty], updatedBy)
		select @CountISN, [State], [Order1Qty], [Order2Qty], @updatedBy from @tmpProviderFile

	update Datacount
	set Order1Qty=@Order1Qty, Order2Qty=@Order2Qty,
		TotalOrderQty=@Order1Qty+@Order2Qty,
		OrderFileName='',
		updatedDate=getdate(), updatedBy=@updatedBy
	where CountISN=@CountISN

	return @CountISN
end

GO


USE [PremierDataWarehouse]
GO

/****** Object:  StoredProcedure [dbo].[xp_dataexportdetail_upd]    Script Date: 02/24/18 9:11:16 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[xp_dataexportdetail_upd](@DetailISN int, @ExportFileName varchar(100)=null,
	@CampaignName nvarchar(50)=null, @CampaignDID varchar(20)=null, @PieceQty int=null,
	@ROICampaignISN int=null, @DDSessionID varchar(100)=null, @DDCampaignISN int=-1, @updatedBy int)
as
begin
	declare @ExportISN int, @TotalPieceQty int, @ExportFileNames varchar(2000), @CampaignNames nvarchar(2000), 
		@PieceQtyDetail varchar(200), @CampaignDIDs varchar(200), @DDSessionIDs varchar(2000), @DetailISNs varchar(500)
	 
	select @ExportISN=ExportISN from DataExportDetail where DetailISN=@DetailISN

	update DataExportDetail
	set ExportFileName=isnull(@ExportFileName, ExportFileName),
		CampaignName=isnull(@CampaignName, CampaignName),
		CampaignDID=isnull(@CampaignDID, CampaignDID),
		PieceQty=isnull(@PieceQty, PieceQty),
		ROICampaignISN=isnull(@ROICampaignISN, ROICampaignISN),
		DDSessionID=isnull(@DDSessionID, DDSessionID),
		DDCampaignISN=(case when @DDCampaignISN>0 then @DDCampaignISN else DDCampaignISN end),
		updatedDate=getdate(), updatedBy=@updatedBy
	where DetailISN=@DetailISN

	create table #tmpdetail(ExportFileName varchar(100), CampaignName nvarchar(50), CampaignDID varchar(20), PieceQty int, DDSessionID varchar(100), DetailISN int primary key)
	Insert Into #tmpdetail(ExportFileName, CampaignName, CampaignDID, PieceQty, DDSessionID, DetailISN)
	select ExportFileName, CampaignName, CampaignDID, PieceQty, DDSessionID, DetailISN 
	from DataExportDetail 
	where ExportISN=@ExportISN and DebtQty>0
	 
	select @TotalPieceQty=sum(PieceQty) from #tmpdetail
	set @ExportFileNames = STUFF( (SELECT '»' + ExportFileName FROM #tmpdetail FOR XML PATH ('') ), 1, 1, '')
	set @CampaignNames = STUFF( (SELECT '»' + CampaignName FROM #tmpdetail FOR XML PATH ('') ), 1, 1, '')
	set @CampaignDIDs = STUFF( (SELECT '»' + CampaignDID FROM #tmpdetail FOR XML PATH ('') ), 1, 1, '')
	set @PieceQtyDetail = STUFF( (SELECT '»' + convert(varchar(20), PieceQty) FROM #tmpdetail FOR XML PATH ('') ), 1, 1, '')
	set @DDSessionIDs = STUFF( (SELECT '»' + DDSessionID FROM #tmpdetail FOR XML PATH ('') ), 1, 1, '')
	set @DetailISNs = STUFF( (SELECT '»' + convert(varchar(20), DetailISN) FROM #tmpdetail FOR XML PATH ('') ), 1, 1, '')

	update DataExport 
	set PieceQty=@TotalPieceQty, ExportFileName=@ExportFileNames, 
		CampaignName=@CampaignNames, PieceQtyDetail=@PieceQtyDetail,
		CampaignDID=@CampaignDIDs,
		DDSessionID=@DDSessionIDs, DetailISNs=@DetailISNs
	where ExportISN=@ExportISN

	return @DetailISN
end

GO


USE [PremierDataWarehouse]
GO

/****** Object:  StoredProcedure [dbo].[xp_inventory_export2]    Script Date: 02/24/18 9:12:24 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[xp_inventory_export2](@Debt1Qty int, @Debt2Qty int, @Debt3Qty int, @Debt4Qty int, @Debt5Qty int, @ExpDesc nvarchar(2000)=null,
	@CalculateISN int=null, @UpdatedBy int, @dx int=90)
as
begin
	declare @Until datetime, @TotalQty int, @Debt1TotalQty int, @Debt2TotalQty int, @Debt3TotalQty int, 
		@Debt4TotalQty int, @Debt5TotalQty int, @Qry varchar(max), @ExportISN int

	set @Until=dbo.DateOnly(getdate() - @dx)

	if exists( select * from DataExport where ExportStatus=0 and ExportDate>DATEADD(mi, -30, getdate()) )
		return -2

	Insert Into DataExport(ExportBy, ExpDesc, updatedBy) Values(@updatedBy, @ExpDesc, @updatedBy)
	set @ExportISN = SCOPE_IDENTITY();

	declare @reqExport as table(SettingID int, DebtQty int, OrderQty int default  0)
	Insert Into @reqExport(SettingID, DebtQty) Values(1, @Debt1Qty)
	Insert Into @reqExport(SettingID, DebtQty) Values(2, @Debt2Qty)
	Insert Into @reqExport(SettingID, DebtQty) Values(3, @Debt3Qty)
	Insert Into @reqExport(SettingID, DebtQty) Values(4, @Debt4Qty)
	Insert Into @reqExport(SettingID, DebtQty) Values(5, @Debt5Qty)

	declare @Setting as table(SettingID int primary key, MinAmount money, MaxAmount money)
	Insert Into @Setting(SettingID, MinAmount, MaxAmount)
		select SettingID, isnull(MinAmount,0), isnull(MaxAmount, 100000000) from RateSetting
	declare @tmpSummary as table ([State] varchar(50), SettingID int, StoreQty int)
	Insert Into @tmpSummary([State], SettingID, StoreQty)
	select memState, SettingID, count(*)
	from Member a, @Setting b
	where (LastExportDate<@Until or LastExportDate is null)
		and a.memESTDebt>=b.MinAmount and a.memESTDebt<b.MaxAmount
	group by memState, SettingID

	declare @MinAmount money, @MaxAmount money, @DebtQty int, @OrderQty int, @SettingID int, @StoreTotalQty int,
		@State varchar(20), @StoreQty int, @Qty int, @RemainTotalQty int, @NoRec int, @Count int

	Create Table #tmpISN(MemberISN int primary key, SettingID int)
	declare curexport cursor local for
		select SettingID, DebtQty from @reqExport where DebtQty>0
	open curexport
	fetch next from curexport into @SettingID, @DebtQty
	while(@@FETCH_STATUS=0)
	begin
		set @OrderQty=0
		select @StoreTotalQty=sum(StoreQty) from @tmpSummary where SettingID=@SettingID
		select @MinAmount=MinAmount, @MaxAmount=MaxAmount from @Setting where SettingID=@SettingID

		if(@DebtQty>@StoreTotalQty)
		begin
			set @Qry='Insert Into #tmpISN(MemberISN, SettingID) select MemberISN, ' + convert(varchar(20), @SettingID) + ' from Member where memESTDebt>=' + convert(varchar(20), @MinAmount) + ' and memESTDebt<' + convert(varchar(20), @MaxAmount) + '	and ( LastExportDate is null or LastExportDate<''' + Convert(varchar(30), @Until, 101) + ''' )'
			set @OrderQty = (@DebtQty-@StoreTotalQty)
			print @Qry
			--exec (@Qry)
		end
		else
		begin
			select @NoRec = count(*) from @tmpSummary where StoreQty>0 and SettingID=@SettingID
			set @Count=1
			set @RemainTotalQty=@DebtQty

			declare cur1 cursor local for
				select [State], StoreQty from @tmpSummary where StoreQty>0 and SettingID=@SettingID order by StoreQty
			open cur1
			fetch next from cur1 into @State, @StoreQty
			while(@@FETCH_STATUS=0)
			begin
				if(@Count<@NoRec)
				begin
					set @Qty = round(convert(float,@DebtQty)*@StoreQty/@StoreTotalQty,0)
					
					if (@Qty>0)
					begin
						if(@Qty>@StoreQty) set @Qty=@StoreQty

						if(@Qty>@RemainTotalQty) set @Qty=@RemainTotalQty

						set @Qry='Insert Into #tmpISN(MemberISN, SettingID) select top ' + convert(varchar(20), @Qty) + ' MemberISN, ' + convert(varchar(20), @SettingID) + ' from Member where memESTDebt>=' + convert(varchar(20), @MinAmount) + ' and memESTDebt<' + convert(varchar(20), @MaxAmount) + ' and ( LastExportDate is null or LastExportDate<''' + Convert(varchar(30), @Until, 101) + ''' ) and memState=''' + @State + ''' order by LastExportDate desc ' 
						print @Qry
						--exec (@Qry)

						set @RemainTotalQty = @RemainTotalQty - @Qty
					end

					if(@RemainTotalQty=0) break;
				end
				else
				begin
					if(@Qry<>'') set @Qry = @Qry +  char(13) + char(10) + ' union '  +  char(13) + char(10)

					set @Qry='Insert Into #tmpISN(MemberISN, SettingID) select top ' + convert(varchar(20), @RemainTotalQty) + ' MemberISN, ' + convert(varchar(20), @SettingID) + ' from Member where memESTDebt>=' + convert(varchar(20), @MinAmount) + ' and memESTDebt<' + convert(varchar(20), @MaxAmount) + ' and ( LastExportDate is null or LastExportDate<''' + Convert(varchar(30), @Until, 101) + ''' ) and memState=''' + @State + ''' order by LastExportDate desc ' 
					print @Qry
					--exec (@Qry)
				end

				set @Count=@Count+1
				fetch next from cur1 into @State, @StoreQty
			end
			close cur1
			deallocate cur1
		end

		update @reqExport set OrderQty=@OrderQty where SettingID=@SettingID

		fetch next from curexport into @SettingID, @DebtQty
	end
	close curexport
	deallocate curexport

	declare @tmpexportqty as table (SettingID int, DebtQty int)
	Insert Into @tmpexportqty(SettingID, DebtQty)
		select SettingID, count(*)
		from #tmpISN
		group by SettingID

	update Member 
	set LastExportISN=@ExportISN, LastExportDate=getdate()
	where MemberISN in(select MemberISN from #tmpISN)

	Insert Into MemberOfExport(MemberISN, ExportISN) select MemberISN, @ExportISN from #tmpISN

	Insert Into DataExportDetail(ExportISN, SettingID, DebtQty, OrderQty)
	select @ExportISN, a.SettingID, a.DebtQty, b.OrderQty
	from @tmpexportqty a, @reqExport b
	where a.SettingID=b.SettingID

	update DataExport
	set ExpTotalQty = ( select sum(DebtQty) from @tmpexportqty ),
		ExportStatus = 1
	where ExportISN=@ExportISN

	return @ExportISN
end

GO


USE [PremierDataWarehouse]
GO

/****** Object:  StoredProcedure [dbo].[xp_inventory_export]    Script Date: 02/24/18 9:12:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[xp_inventory_export](@xmlRequest nvarchar(2000), @ExpDesc nvarchar(2000)=null, @CalculateISN int=null, @UpdatedBy int, @dx int=90)
as
begin
	declare @Until datetime, @Qry varchar(max), @ExportISN int, @idoc int

	set @Until=dbo.DateOnly(getdate() - @dx)

	if exists( select * from DataExport where ExportStatus=0 and ExportDate>DATEADD(mi, -5, getdate()) )
		return -2

	Insert Into DataExport(ExportBy, ExpDesc, updatedBy) Values(@updatedBy, @ExpDesc, @updatedBy)
	set @ExportISN = SCOPE_IDENTITY();

	declare @reqExport as table(SettingID int, DebtQty int, OrderQty int default  0)
	exec sp_xml_preparedocument @idoc OUTPUT, @xmlRequest
	Insert Into @reqExport(SettingID, DebtQty)
	select SettingID, DebtQty
	from OPENXML(@idoc, '/Root/Info') 
	WITH (SettingID int, DebtQty int)
	exec sp_xml_removedocument @idoc

	Create table #tmpSetting(SettingID int primary key, DebtLoad nvarchar(200), MinAmount money, MaxAmount money)
	Insert Into #tmpSetting(SettingID, DebtLoad, MinAmount, MaxAmount)
		select SettingID, DebtLoad, isnull(MinAmount,0), (case when MaxAmount<=0 or MaxAmount is null then 100000000 else MaxAmount end)  
		from RateSetting

	Create table #tmpSummary([State] varchar(50), SettingID int, StoreQty int)
	Insert Into #tmpSummary([State], SettingID, StoreQty)
	select memState, SettingID, count(*)
	from Member a, #tmpSetting b
	where (LastExportDate<@Until or LastExportDate is null)
		and a.memESTDebt>=b.MinAmount and a.memESTDebt<=b.MaxAmount
	group by memState, SettingID

	Create table #tmpSummary2 (SettingID int, StoreQty int)
	Insert Into #tmpSummary2(SettingID, StoreQty)
		select SettingID, sum(StoreQty) from #tmpSummary group by SettingID

	if exists(select * from @reqExport a left join #tmpSummary2 b on a.SettingID=b.SettingID where DebtQty>isnull(b.StoreQty,0))
	begin
		delete from DataExport where ExportISN=@ExportISN
		return -3
	end

	declare @MinAmount money, @MaxAmount money, @DebtQty int, @OrderQty int, @SettingID int, @StoreTotalQty int,
		@State varchar(20), @StoreQty int, @Qty int, @RemainTotalQty int, @NoRec int, @Count int

	Create Table #tmpISN(MemberISN int primary key, SettingID int)
	declare curexport cursor local for
		select SettingID, DebtQty from @reqExport where DebtQty>0
	open curexport
	fetch next from curexport into @SettingID, @DebtQty
	while(@@FETCH_STATUS=0)
	begin
		set @OrderQty=0
		select @StoreTotalQty=StoreQty from #tmpSummary2 where SettingID=@SettingID
		select @MinAmount=MinAmount, @MaxAmount=MaxAmount from #tmpSetting where SettingID=@SettingID

		if(@DebtQty>@StoreTotalQty)
		begin
			set @Qry='Insert Into #tmpISN(MemberISN, SettingID) select MemberISN, ' + convert(varchar(20), @SettingID) + ' from Member where memESTDebt>=' + convert(varchar(20), @MinAmount) + ' and memESTDebt<=' + convert(varchar(20), @MaxAmount) + '	and ( LastExportDate is null or LastExportDate<''' + Convert(varchar(30), @Until, 101) + ''' )'
			set @OrderQty = (@DebtQty-@StoreTotalQty)
			--print @Qry
			exec (@Qry)
		end
		else
		begin
			select @NoRec = count(*) from #tmpSummary where StoreQty>0 and SettingID=@SettingID
			set @Count=1
			set @RemainTotalQty=@DebtQty

			declare cur1 cursor local for
				select [State], StoreQty from #tmpSummary where StoreQty>0 and SettingID=@SettingID order by StoreQty
			open cur1
			fetch next from cur1 into @State, @StoreQty
			while(@@FETCH_STATUS=0)
			begin
				if(@Count<@NoRec)
				begin
					set @Qty = round(convert(float,@DebtQty)*@StoreQty/@StoreTotalQty,0)
					
					if (@Qty>0)
					begin
						if(@Qty>@StoreQty) set @Qty=@StoreQty

						if(@Qty>@RemainTotalQty) set @Qty=@RemainTotalQty

						set @Qry='Insert Into #tmpISN(MemberISN, SettingID) select top ' + convert(varchar(20), @Qty) + ' MemberISN, ' + convert(varchar(20), @SettingID) + ' from Member where memESTDebt>=' + convert(varchar(20), @MinAmount) + ' and memESTDebt<=' + convert(varchar(20), @MaxAmount) + ' and ( LastExportDate is null or LastExportDate<''' + Convert(varchar(30), @Until, 101) + ''' ) and memState=''' + @State + ''' order by LastExportDate desc ' 
						--print @Qry
						exec (@Qry)

						set @RemainTotalQty = @RemainTotalQty - @Qty
					end

					if(@RemainTotalQty=0) break;
				end
				else
				begin
					if(@Qry<>'') set @Qry = @Qry +  char(13) + char(10) + ' union '  +  char(13) + char(10)

					set @Qry='Insert Into #tmpISN(MemberISN, SettingID) select top ' + convert(varchar(20), @RemainTotalQty) + ' MemberISN, ' + convert(varchar(20), @SettingID) + ' from Member where memESTDebt>=' + convert(varchar(20), @MinAmount) + ' and memESTDebt<=' + convert(varchar(20), @MaxAmount) + ' and ( LastExportDate is null or LastExportDate<''' + Convert(varchar(30), @Until, 101) + ''' ) and memState=''' + @State + ''' order by LastExportDate desc ' 
					--print @Qry
					exec (@Qry)
				end

				update DataExport set ExportDate=getdate() where ExportISN=@ExportISN

				set @Count=@Count+1
				fetch next from cur1 into @State, @StoreQty
			end
			close cur1
			deallocate cur1
		end

		update DataExport set ExportDate=getdate() where ExportISN=@ExportISN

		update @reqExport set OrderQty=@OrderQty where SettingID=@SettingID

		fetch next from curexport into @SettingID, @DebtQty
	end
	close curexport
	deallocate curexport

	begin tran

	Create Table #tmpexportqty(SettingID int, DebtQty int)
	Insert Into #tmpexportqty(SettingID, DebtQty)
		select SettingID, count(*)
		from #tmpISN
		group by SettingID

	update DataExport set ExportDate=getdate() where ExportISN=@ExportISN

	update Member 
	set LastExportISN=@ExportISN, LastExportDate=getdate()
	where MemberISN in(select MemberISN from #tmpISN)

	update DataExport set ExportDate=getdate() where ExportISN=@ExportISN

	Insert Into MemberOfExport(MemberISN, ExportISN) select MemberISN, @ExportISN from #tmpISN

	update DataExport set ExportDate=getdate() where ExportISN=@ExportISN

	update #tmpSetting set DebtLoad=replace(replace(replace(replace(replace(replace(DebtLoad, '>', ''), '<', ''), '=', ''), '$', ''), '.0', ''), ' ','')

	Insert Into DataExportDetail(ExportISN, SettingID, DebtLoad, MinAmount, MaxAmount, DebtQty, OrderQty, ExportFileName)
	select @ExportISN, a.SettingID, DebtLoad, MinAmount, MaxAmount, a.DebtQty, b.OrderQty, 'Exp_' + DebtLoad + '_' + convert(varchar(20),  a.DebtQty) + '.csv'
	from #tmpexportqty a, @reqExport b, #tmpSetting c
	where a.SettingID=b.SettingID and a.SettingID=c.SettingID
	order by a.SettingID

	declare @ExportFileName varchar(2000)
	set @ExportFileName = STUFF( (SELECT '»' + ExportFileName FROM DataExportDetail where ExportISN=@ExportISN and DebtQty>0 FOR XML PATH ('') ), 1, 1, '')

	update DataExport
	set ExpTotalQty = ( select sum(DebtQty) from #tmpexportqty ),
		ExportFileName = @ExportFileName,
		ExportStatus = 1
	where ExportISN=@ExportISN

	commit tran

	return @ExportISN
end

GO


USE [PremierDataWarehouse]
GO

/****** Object:  StoredProcedure [dbo].[xp_member_ins]    Script Date: 02/24/18 9:14:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[xp_member_ins](@memFirstName nvarchar(30), @memMiddleName nvarchar(30), @memLastName nvarchar(30), @memState nvarchar(50),
	@memSSN varchar(20)=null, @memEmail nvarchar(50)=null, @memAddress nvarchar(50)=null, @memCity nvarchar(50)=null, 
	@memZip varchar(15)=null, @memSuffix nvarchar(50)=null, @memPhone varchar(20)=null, @memFax varchar(20)=null,
	@memHomePhone varchar(20), @memMobilePhone varchar(20),	@memWorkPhone varchar(20), @memESTDebt money=0,
	@CountISN int=null, @updatedBy int)
as
begin
	declare @MemberISN int

	Insert Into Member(memFirstName, memMiddleName, memLastName, memSSN, memEmail, memAddress, memCity, memState, memZip, 
		memSuffix, memPhone, memFax, memHomePhone, memMobilePhone, memWorkPhone, memSignUp, memESTDebt, 
		CountISN, LastExportISN, LastExportDate, updatedBy)
	Values(@memFirstName, @memMiddleName, @memLastName, @memSSN, @memEmail, @memAddress, @memCity, @memState, @memZip, 
		@memSuffix, @memPhone, @memFax, @memHomePhone, @memMobilePhone, @memWorkPhone, getdate(), @memESTDebt, 
		@CountISN, null, null, @updatedBy)

	set @MemberISN=SCOPE_IDENTITY()
	
	return @MemberISN
end

GO


USE [PremierDataWarehouse]
GO

/****** Object:  StoredProcedure [dbo].[xp_inventory_getinfo]    Script Date: 02/24/18 9:14:10 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[xp_inventory_getinfo](@ExportISN int=0, @dx int=90)
as
begin
	declare @Until datetime, @TotalQty int, @Debt1TotalQty int, @Debt2TotalQty int, @Debt3TotalQty int, @Debt4TotalQty int, @Debt5TotalQty int
	set @Until=dbo.DateOnly(getdate() - @dx)

	Create table #tmpSetting(SettingID int primary key, MinAmount money, MaxAmount money)
	Insert Into #tmpSetting(SettingID, MinAmount, MaxAmount)
		select SettingID, isnull(MinAmount,0), (case when MaxAmount<=0 or MaxAmount is null then 100000000 else MaxAmount end) 
		from RateSetting

	Create table #tmpSummary([State] varchar(50), SettingID int, DebtQty int)
	Insert Into #tmpSummary([State], SettingID, DebtQty)
	select memState, SettingID, count(*)
	from Member a, #tmpSetting b
	where (LastExportDate<@Until or LastExportDate is null)
		and a.memESTDebt>=b.MinAmount and a.memESTDebt<=b.MaxAmount
	group by memState, SettingID

	select * from #tmpSummary

	select * from [State]

	select * from RateSetting

	drop table #tmpSetting; drop table #tmpSummary
	return 0
end

GO


USE [PremierDataWarehouse]
GO

/****** Object:  StoredProcedure [dbo].[xp_parameter_upd]    Script Date: 02/24/18 9:14:56 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[xp_parameter_upd](@ParamID varchar(20), @Value varchar(100))
as
begin
	if exists(select * from Parameter where [Param]=@ParamID)
		Update Parameter set Value=@Value where [Param]=@ParamID
	else
		Insert Into Parameter([Param], Value) Values(@ParamID, @Value)
end

GO


USE [PremierDataWarehouse]
GO

/****** Object:  StoredProcedure [dbo].[xp_member_upload]    Script Date: 02/24/18 9:14:53 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[xp_member_upload](@tbMem as MemberType readonly, @CountISN int=null, @updatedBy int)
as
begin
	declare @LastISN int, @NoRec int

	select @LastISN=max(MemberISN) from Member

	Insert Into Member(memFirstName, memMiddleName, memLastName, memSSN, memEmail, memAddress, memCity, memState, memZip, 
		memSuffix, memPhone, memFax, memHomePhone, memMobilePhone, memWorkPhone, memSignUp, memESTDebt, 
		CountISN, LastExportISN, LastExportDate, updatedBy)
	select FirstName, MiddleName, LastName, SSN, Email, Address, City, State, Zip, 
		Suffix, Phone, Fax, '', '', '', getdate(), ESTDebt , 
		@CountISN, null, null, @updatedBy
	from @tbMem

	set @NoRec = @@ROWCOUNT
	
	update DataCount
	set TotalReceivedQty=isnull(TotalReceivedQty,0) + @NoRec, 
		updatedDate=getdate(), updatedBy=@updatedBy
	where CountISN=@CountISN

	return @CountISN
end

GO


USE [PremierDataWarehouse]
GO

/****** Object:  StoredProcedure [dbo].[xp_string_split]    Script Date: 02/24/18 9:15:17 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[xp_string_split](@lstISN varchar(8000), @tbname varchar(100), @delimiter char(1)=',')
as
begin
	declare @qry varchar(1000), @pos int, @obj varchar(100)
	exec (@qry); set @pos=1
	while(@pos>0 and @lstISN<>'')
	begin
		set @pos = charindex(@delimiter,@lstISN)
		if (@pos = 0)
			set @obj = @lstISN
		else
		begin
			set @obj = left(@lstISN,@pos - 1)
			set @lstISN = right(@lstISN,len(@lstISN)- @pos)
		end
		if(@obj<>'')
		begin
			set @qry='Insert Into ' + @tbname + '(Info) values(''' + ltrim(rtrim(@obj)) + ''')'
			exec (@qry)
		end
	end
end

GO


USE [PremierDataWarehouse]
GO

/****** Object:  StoredProcedure [dbo].[xp_ratesetting_upd]    Script Date: 02/24/18 9:15:12 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[xp_ratesetting_upd](@XmlSetting varchar(8000), @SpliterSettingID int, @updatedBy int)
as
begin
	declare @idoc int, @sid int, @n int, @minAmount2 money, @maxAmount1 money

	declare @tmpdebtload as table (SettingID int, DebtLoad nvarchar(200), LeadProjected float, LeadPortfolio float, MinAmount money, MaxAmount money)

	exec sp_xml_preparedocument @idoc OUTPUT, @XmlSetting
	Insert Into @tmpdebtload(SettingID, LeadProjected, LeadPortfolio, DebtLoad, MinAmount, MaxAmount)
	select SettingID, LeadProjected, LeadPortfolio, DebtLoad, MinAmount, MaxAmount
	from OPENXML(@idoc, '/Root/DebtLoad') 
	WITH (SettingID int, LeadProjected float, LeadPortfolio float, DebtLoad nvarchar(200), MinAmount money, MaxAmount money)
	exec sp_xml_removedocument @idoc

	if( (select sum(LeadPortfolio) from @tmpdebtload)<>100 ) return -2
	
	select @n=count(*) from @tmpdebtload
	set @sid=1
	while(@sid<@n)
	begin
		select @maxAmount1=MaxAmount from @tmpdebtload where SettingID=@sid
		select @minAmount2=MinAmount from @tmpdebtload where SettingID=@sid+1
		if(@maxAmount1>=@minAmount2) return -3
		set @sid=@sid+1
	end

	update RateSetting
	set LeadProjected=a.LeadProjected, LeadPortfolio=a.LeadPortfolio,
		DebtLoad=a.DebtLoad, MinAmount=a.MinAmount, MaxAmount=a.MaxAmount,
		updatedDate=getdate(), updatedBy=@updatedBy
	from @tmpdebtload a
	where RateSetting.SettingID=a.SettingID

	exec xp_parameter_upd 'SpliterSettingID', @SpliterSettingID

	return 0
end

GO


SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[Ten Most Expensive Products] AS
SET ROWCOUNT 10
SELECT Products.ProductName AS TenMostExpensiveProducts, Products.UnitPrice
FROM Products
ORDER BY Products.UnitPrice DESC

GO