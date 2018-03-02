USE [PremierDataWarehouse]
GO

/****** Object:  Table [dbo].[CalculateHistory]    Script Date: 02/24/18 9:07:47 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CalculateHistory](
	[HistoryISN] [int] IDENTITY(1,1) NOT NULL,
	[CalculateDate] [datetime] NULL DEFAULT (getdate()),
	[PercentSalesRepresentative] [float] NULL,
	[NewLeadPerSalesRepresentativeDay] [float] NULL,
	[OpenerAppConversionRate] [float] NULL,
	[CallstoOpenerDay] [float] NULL,
	[WorkdaysWeek] [float] NULL,
	[NoOfSalesRepresentative] [float] NULL,
	[NoOfOpenerNeeded] [float] NULL,
	[TotalCallsWeek] [float] NULL,
	[updatedDate] [datetime] NULL DEFAULT (getdate()),
	[updatedBy] [int] NULL,
	[DebtLoadDesc] [nvarchar](max) NULL,
	[CountISN] [int] NULL,
	[TotalPieces] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[HistoryISN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[CalculateHistory]  WITH CHECK ADD FOREIGN KEY([CountISN])
REFERENCES [dbo].[DataCount] ([CountISN])
GO


USE [PremierDataWarehouse]
GO

/****** Object:  Table [dbo].[CalculateHistoryDetail]    Script Date: 02/24/18 9:08:11 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CalculateHistoryDetail](
	[DetailISN] [int] IDENTITY(1,1) NOT NULL,
	[HistoryISN] [int] NULL,
	[SettingID] [int] NULL,
	[DebtLoad] [nvarchar](200) NULL,
	[LeadProjected] [float] NULL,
	[MinAmount] [money] NULL,
	[MaxAmount] [money] NULL,
	[Leads] [int] NULL,
	[MailPercent] [float] NULL,
	[PieceQty] [int] NULL,
	[updatedDate] [datetime] NULL DEFAULT (getdate()),
	[updatedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[DetailISN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CalculateHistoryDetail]  WITH CHECK ADD FOREIGN KEY([HistoryISN])
REFERENCES [dbo].[CalculateHistory] ([HistoryISN])
ON DELETE CASCADE
GO


USE [PremierDataWarehouse]
GO

/****** Object:  Table [dbo].[DataCountProviderDetailFile]    Script Date: 02/24/18 9:18:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DataCountProviderDetailFile](
	[DetailISN] [int] IDENTITY(1,1) NOT NULL,
	[CountISN] [int] NULL,
	[State] [varchar](50) NULL,
	[Debt1Qty] [int] NULL,
	[Debt1Percent] [float] NULL,
	[Debt2Qty] [int] NULL,
	[Debt2Percent] [float] NULL,
	[Debt3Qty] [int] NULL,
	[Debt3Percent] [float] NULL,
	[TotalQty] [int] NULL,
	[updatedDate] [datetime] NULL DEFAULT (getdate()),
	[updatedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[DetailISN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[DataCountProviderDetailFile]  WITH CHECK ADD FOREIGN KEY([CountISN])
REFERENCES [dbo].[DataCount] ([CountISN])
ON DELETE CASCADE
GO


USE [PremierDataWarehouse]
GO

/****** Object:  Table [dbo].[DataCount]    Script Date: 02/24/18 9:19:02 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DataCount](
	[CountISN] [int] IDENTITY(1,1) NOT NULL,
	[CountName] [nvarchar](100) NULL,
	[CriteriaFiles] [varchar](500) NULL,
	[ProviderTotalRecords] [int] NULL,
	[ProviderFiles] [varchar](500) NULL,
	[Description] [nvarchar](500) NULL,
	[addedDate] [datetime] NULL DEFAULT (getdate()),
	[addedby] [int] NULL,
	[updatedDate] [datetime] NULL DEFAULT (getdate()),
	[updatedBy] [int] NULL,
	[TotalOrderQty] [int] NULL DEFAULT ((0)),
	[OrderFileName] [varchar](100) NULL,
	[DataReceivedFiles] [varchar](500) NULL,
	[TotalReceivedQty] [int] NULL DEFAULT ((0)),
	[Status] [tinyint] NULL DEFAULT ((0)),
	[Order1Qty] [int] NULL DEFAULT ((0)),
	[Order2Qty] [int] NULL DEFAULT ((0)),
PRIMARY KEY CLUSTERED 
(
	[CountISN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


USE [PremierDataWarehouse]
GO

/****** Object:  Table [dbo].[DataExportDetail]    Script Date: 02/24/18 9:20:39 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DataExportDetail](
	[DetailISN] [int] IDENTITY(1,1) NOT NULL,
	[ExportISN] [int] NULL,
	[SettingID] [int] NULL,
	[MinAmount] [money] NULL,
	[MaxAmount] [money] NULL,
	[DebtQty] [int] NULL,
	[OrderQty] [int] NULL,
	[DebtLoad] [nvarchar](200) NULL,
	[ExportFileName] [varchar](100) NULL,
	[CampaignName] [nvarchar](50) NULL,
	[CampaignDID] [varchar](20) NULL,
	[PieceQty] [int] NULL DEFAULT ((0)),
	[ROICampaignISN] [int] NULL,
	[DDSessionID] [varchar](100) NULL,
	[updatedDate] [datetime] NULL DEFAULT (getdate()),
	[updatedBy] [int] NULL,
	[DDCampaignISN] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[DetailISN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[DataExportDetail]  WITH CHECK ADD FOREIGN KEY([ExportISN])
REFERENCES [dbo].[DataExport] ([ExportISN])
ON DELETE CASCADE
GO


USE [PremierDataWarehouse]
GO

/****** Object:  Table [dbo].[MemberOfExport]    Script Date: 02/24/18 9:20:55 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MemberOfExport](
	[MemberISN] [int] NOT NULL,
	[ExportISN] [int] NOT NULL,
	[addedDate] [datetime] NULL DEFAULT (getdate()),
	[IsUpload] [tinyint] NULL DEFAULT ((0)),
 CONSTRAINT [pk_memberofexport_primarykey] PRIMARY KEY CLUSTERED 
(
	[MemberISN] ASC,
	[ExportISN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[MemberOfExport]  WITH CHECK ADD FOREIGN KEY([ExportISN])
REFERENCES [dbo].[DataExport] ([ExportISN])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[MemberOfExport]  WITH CHECK ADD FOREIGN KEY([MemberISN])
REFERENCES [dbo].[Member] ([MemberISN])
ON DELETE CASCADE
GO


USE [PremierDataWarehouse]
GO

/****** Object:  View [dbo].[Vw_DataCount]    Script Date: 02/24/18 9:22:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create View [dbo].[Vw_DataCount]
as
	select a.*, empUserName as updatedName,
		isnull(TotalDebt1Qty,0) as TotalDebt1Qty, isnull(TotalDebt2Qty, 0) as TotalDebt2Qty,
		( isnull(TotalDebt1Qty,0) + isnull(TotalDebt2Qty, 0) ) as TotalQty
	from DataCount a left join Employee b on a.updatedBy=b.EmployeeISN
		left join (select CountISN, sum(Debt1Qty) as TotalDebt1Qty, sum(Debt2Qty) as TotalDebt2Qty
					from DataCountProviderDetailFile
					group by CountISN
					) c on a.CountISN=c.CountISN


GO


USE [PremierDataWarehouse]
GO

/****** Object:  View [dbo].[Vw_CalculateHistory]    Script Date: 02/24/18 9:21:54 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create View [dbo].[Vw_CalculateHistory]
as
	select a.*, empUserName as updatedName
	from CalculateHistory a left join Employee b on a.updatedBy=b.EmployeeISN


GO


USE [PremierDataWarehouse]
GO

/****** Object:  View [dbo].[Vw_DataExportDetail]    Script Date: 02/24/18 9:23:07 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create View [dbo].[Vw_DataExportDetail]
as
	select a.*, d.empUserName as updatedName, c.empUsername as exportName
	from DataExportDetail a join DataExport b on a.ExportISN=b.ExportISN
		 left join Employee d on a.updatedBy=d.EmployeeISN
		left join Employee c on b.ExportBy=c.EmployeeISN

GO

USE [PremierDataWarehouse]
GO

/****** Object:  UserDefinedTableType [dbo].[MemberType]    Script Date: 02/24/18 9:00:50 AM ******/
CREATE TYPE [dbo].[MemberType] AS TABLE(
	[FirstName] [nvarchar](30) NULL,
	[MiddleName] [nvarchar](30) NULL,
	[LastName] [nvarchar](30) NULL,
	[SSN] [varchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Zip] [varchar](15) NULL,
	[Suffix] [nvarchar](50) NULL,
	[Phone] [varchar](20) NULL,
	[Fax] [varchar](20) NULL,
	[ESTDebt] [money] NULL DEFAULT ((0))
)
GO


