học javascript, MVC
space trong html &nbsp;
tab trong html &emsp;
Server.HtmlEncode(cellDescription.Text);//convert string to html
Server.HtmlDecode(GridView1.Rows[j].Cells[i].Text).Replace("<br />", "\n");
Làm tròn 8 số
Math.Round(ConvertToDecimal(var), 8)
Math.Round(ConvertToDecimal(Eval("NoOfLead"))/ConvertToDecimal(Eval("WorkingTime")), 8)

Thêm bôi đen khi tạo page mới, menuData3, TemplateLogon, TemplateSummary trong premier

Project Premier
https://premierdocprep.com/admin	
Quản lý thông tin nhân viên của công ty, phân công nhiệm vụ để nhân viên công ty có thể tương tác với khách hàng. 
Trang về tài chính, cung cấp dịch vụ tài chính, 

Project Data Warehouse backend
https://picofinancial.com/datawarehouse/	
Page quản lý kho, tính toán số lượng khách hàng theo thời gian, và điều kiện đi kèm( theo tiểu bang, theo số tiền nợ.)
Sử dụng Source Vault  là Source Control để quản lý code. 																

\\192.168.100.33\Websites\DebtExtesion project RoiDealer
Debt_Ext/Code/PremierBE_V2
CMS là 403
Premier là 103
var info = new Common.ProfileLead().GetProfileLead(memberISN);
info.UserLeadTabMain.InterestedProducts.Equals("403")  
                 										
<input type="checkbox" id="chkAmountOfRefundOther" runat="server" onclick="chkAmountOfRefundOther_Click()"/> Other <input type="text" id="txtAmountOfRefundOtherText" class="money" runat="server" disabled="disabled"/>                    
<script>
	function chkAmountOfRefundOther_Click()
	{
	   if( $("[id$='chkAmountOfRefundOther']").is(':checked') ) {
	   
			$("[id$='txtAmountOfRefundOtherText']").prop("disabled", false);
		}
		else
		{
		   $("[id$='txtAmountOfRefundOtherText']").prop("disabled", true);
		}
	}  
</script>

System.Configuration.ConfigurationSettings.AppSettings["SaveFolderURL"]
lấy đường dẫn từ config
this.sDocFileName_URL = fileURL.Replace("\\", "/");
 
OnClientClick="OnSaveData()
function OnSaveData(){
	//lay json
	var jsonData = JSON.stringify(ko.toJS(Credentials.listCredentialsModel));
	$("#<%=txtJsonData.ClientID %>").val(jsonData);
}
	
thanh sort HasSortSelector="true"
hiển thị thanh sorted by trên xtb

string sField = xtb.SortField;
string sDirection = (this.xtb.SortDirection.ToString() == "Ascending") ? "asc" : "desc";
DataRow[] arrRow = dSet.Tables[0].Select("", sField + " " + sDirection);
Thêm điều kiện sort vào trong DataRow.

$(document).ready(function () {
   function formatAMPM(InterestedDate);
});
Tự đông load khi hàm bên trong thay đổi

Hình Loading khi upload file
function LoadingData() {
	var maxWidth = screen.width;
	var maxHeight = screen.height;
	var $windown = window.parent;
	var top = Math.max(0, ((maxHeight - 100) / 2) + ($($windown).scrollTop())) + "px";
	var left = Math.max(0, ((maxWidth - 100) / 2) + ($($windown).scrollLeft())) + "px";
	var strHtmlImg = "<img src='Css/img/loading.gif' style='width:100px;height:100px;position:absolute;top:" + top + ";left:" + left + ";'/>";
	var strHtml = "<div id='loadingData' class='backgroup-custom' style='position: absolute;z-index:10; left: 0px; top: 0px; opacity: 0.4; width: " + $($windown).width() + "px; height: " + $($windown).height() + "px; background-color: rgb(170, 170, 170);'>"+ strHtmlImg +"</div>";
	$('body', $windown.document).before(strHtml);
}
function LoadingDataClose(){
	//debugger;
	var $windown = window.parent;
	$('#loadingData', $windown.document).remove();
}
	
Thêm cột vào DataSet
private DataSet ReformatData(DataSet ds)
{
	try
	{
		if (Common.HtmlHelper.IsEmptyDataSet(ds)) return ds;
		ds.Tables[0].Columns.Add("UserNameFull", typeof(string));

		for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
		{
			DataRow row = ds.Tables[0].Rows[i];
			var textFull = row["UserName"] + "(" + row["memFullName"] + ")";
			row["UserNameFull"] = textFull;
			if (row["UserType"].ToString().Equals("Salesman"))
			{
				ds.Tables[0].Rows[i].Delete(); //Xóa dòng
			}
		}

		ds.AcceptChanges();
		return ds;
	}
	catch (Exception ex) { }
	return ds;
}
		
Thêm cột và thêm dòng vào datatable
DataSet dSet = GetDataFromExcel(pathFull);
int count = 0;
if (dSet != null && dSet.Tables.Count > 0 && dSet.Tables[0].Rows.Count > 0)
{
	DataTable tbMember = new DataTable();
	tbMember.Columns.Add("FirstName");
	tbMember.Columns.Add("MiddleName");
	tbMember.Columns.Add("LastName");
	tbMember.Columns.Add("SSN");
	tbMember.Columns.Add("Email");

	tbMember.Columns.Add("Address");
	tbMember.Columns.Add("City");
	tbMember.Columns.Add("State");
	tbMember.Columns.Add("Zip");
	tbMember.Columns.Add("Suffix");

	tbMember.Columns.Add("Phone");
	tbMember.Columns.Add("Fax");
	tbMember.Columns.Add("ESTDebt");                                                               
	   
	foreach (DataRow row in dSet.Tables[0].Rows)
	{
		DataRow rTB = tbMember.NewRow();
		rTB["FirstName"] = row["FNAME"].ToString();
		rTB["MiddleName"] = row["MI"].ToString();
		rTB["LastName"] = row["LNAME"].ToString();
		rTB["SSN"] = DBNull.Value;
		rTB["Email"] = DBNull.Value;
		
		rTB["Address"] = row["ADDRESS"].ToString();
		rTB["City"] = row["CITY"].ToString();
		rTB["State"] = row["STATE"].ToString();
		rTB["Zip"] = row["ZIP"].ToString() + row["ZIP4"].ToString();
		rTB["Suffix"] = row["SUFFIX"].ToString();
		
		rTB["Phone"] = DBNull.Value;
		rTB["Fax"] = DBNull.Value;
		rTB["ESTDebt"] = Functions.ConvertObjectToDecimal(row["EST DEBT"].ToString());

		tbMember.Rows.Add(rTB);
	}
}

Thêm dòng đầu tiên vào datatable								
DataRow rTB = ds.Tables[0].NewRow();
rTB["MemberISN"] = DBNull.Value;
rTB["UserNameFull"] = " ---------- All ---------- ";
ds.Tables[0].Rows.InsertAt(rTB, 0);
											
Import nhiều record vào database, sử dụng ajax, chuyển qua trang khác xử lý, để trang hiện tại không bị delay, 
Store insert dạng xml hoặc store insert dạng table để cải thiện tốc độ, 
Tạo datatable có field giống filed store, nắm data từ file import vào datatable, import db bằng store.
75k record 8s, 9 file có data, 3 field null,
300k record 22s

Project Data Warehouse backend

DataSet dTotal = db.ExecuteQuery("select TotalQty from DataCountProviderDetailFile where CountISN=" + args.RowData["CountISN"] + " and State='TOTAL'"); 
string ProviderTotal = string.Empty;                   
//string aa = dataRows[0].ToString();
foreach (DataRow item in dTotal.Tables[0].Rows)
{
	ProviderTotal = "[" + item["TotalQty"].ToString() + "]";
}

export dữ liệu từ database, lưu trữ file thêm ngày tháng, tách thư mục, tăng tốc độ truy cập cập thư mục.

hàm upload data từ file excel, csv sử dụng ajax để xử lý. 

<asp:FileUpload ID="FileReceivedUpload" runat="server" />
<script>
	function UploadFileReceived(files) {

		var size = 25 * 1024 * 1024;
		if (files.size > size) {
			alert("File size less 25MB.");            
		}
		else
		{
			var lblFileName = "";
			var CountISN = $("#<%=hiddenCountISN.ClientID %>").val();               
			var sUrl = "MyCallBack.aspx?cmd=uploadfile&countisn=" + CountISN;
			var vFormData = new FormData();
			vFormData.append("name", files.name);
			vFormData.append("file", files);
			$("#ItemCell" + CountISN).html("Uploading...");            
			setTimeout(function () {                               
				$.ajax({
					type: "POST",
					async: false, /// important to fix bug IE
					url: sUrl,
					contentType: false,
					processData: false,
					data: vFormData
				}).done(function (data) {
					if (data != "0") {
						$("#ItemCell" + CountISN).html("[Upload]");
					}
					if (data == "0") {
						location.href = location.href;
					}
					else if (data.indexOf('1') >= 0) {                       
						var arr = data.split(",");
						for (var i = 1; i < arr.length; i++) {
							lblFileName += arr[i] + ", ";
						}                    
						$(".msgError").html(lblFileName + " not found.");                        
					}
					else if (data == "2")                    
						$(".msgError").html("Can't import in Database");
					else if (data == "3")
						$(".msgError").html("Have problem when import");
					else                    
						$(".msgError").html("File is not data");
				  
					//setTimeout(function () { location.reload(1); }, 3000);                    
				});
			}, 100);
			//$("#ItemCell" + CountISN).replaceWith("<p style='color: #009347;' id='ItemCell" + CountISN + "'>Uploading...</p>");
		}        
	}
    $("#<%=FileReceivedUpload.ClientID %>").on('change', function (event) {
        files = event.target.files[0];
        if (files.name.toLowerCase().indexOf(".csv") > -1 || files.name.toLowerCase().indexOf(".xls") > -1 || files.name.toLowerCase().indexOf(".xlsx") > -1)
            UploadFileReceived(files);
        else
            alert("File is not right format.");            
    });
</script>

protected void Page_Load(object sender, EventArgs e)
{
	if (Request["cmd"] != null && Request["cmd"] != "")
	{
		Response.Clear();
		string result = string.Empty;
		switch (Request["cmd"])
		{                    
			case "uploadfile":
				result = this.UploadFile_FromDataCount(Functions.ConvertObjectToInt(Request["countisn"]));
				Response.Write(result);
				break;
		}
		Response.End();
	}
}

private string UploadFile_FromDataCount(int CountISN)
{
	string result = "0";
	var folderFileReceived = Path.Combine(Config["FilesDir"], this.GetFolderPath_DataCount(CountISN, 2));
	HttpPostedFile httpFile = Request.Files[0];
	var fileNameReceived = Functions.MakeValidFileName(Request["name"]);

	if (!string.IsNullOrEmpty(fileNameReceived))
	{
		if (httpFile.ContentLength > 0)
		{                                       
			if (!Directory.Exists(folderFileReceived)) Directory.CreateDirectory(folderFileReceived);
			var pathFull = Path.Combine(folderFileReceived, fileNameReceived);
			httpFile.SaveAs(pathFull);
			if (!string.IsNullOrEmpty(checkData(pathFull)))
			{
				result = "1," + checkData(pathFull);
				Functions.DeleteFile(pathFull);                         
			}
			else
			{
				try
				{
					DataSet dSet = GetDataFromExcel(pathFull);
					int count = 0;
					if (dSet != null && dSet.Tables.Count > 0 && dSet.Tables[0].Rows.Count > 0)
					{
						DataTable tbMember = new DataTable();
						tbMember.Columns.Add("FirstName");
						tbMember.Columns.Add("MiddleName");
						tbMember.Columns.Add("LastName");
						tbMember.Columns.Add("SSN");
						tbMember.Columns.Add("Email");
						
						tbMember.Columns.Add("Address");
						tbMember.Columns.Add("City");
						tbMember.Columns.Add("State");
						tbMember.Columns.Add("Zip");
						tbMember.Columns.Add("Suffix");

						tbMember.Columns.Add("Phone");
						tbMember.Columns.Add("Fax");
						tbMember.Columns.Add("ESTDebt");                                                               
													  
						

						foreach (DataRow row in dSet.Tables[0].Rows)
						{
							string MiddleName = string.Empty;
							string Suffix = string.Empty;
							string Zip4 = string.Empty;
							DataRow rTB = tbMember.NewRow();
							rTB["FirstName"] = row["FNAME"].ToString();
							rTB["MiddleName"] = row["MI"].ToString();
							rTB["LastName"] = row["LNAME"].ToString();
							rTB["SSN"] = DBNull.Value;
							rTB["Email"] = DBNull.Value;
							
							rTB["Address"] = row["ADDRESS"].ToString();
							rTB["City"] = row["CITY"].ToString();
							rTB["State"] = row["STATE"].ToString();
							rTB["Zip"] = row["ZIP"].ToString() + row["ZIP4"].ToString();
							rTB["Suffix"] = row["SUFFIX"].ToString();
							
							rTB["Phone"] = DBNull.Value;
							rTB["Fax"] = DBNull.Value;
							rTB["ESTDebt"] = Functions.ConvertObjectToDecimal(row["EST DEBT"].ToString());

							//if (dSet.Tables[0].Columns.IndexOf("MI") > 0)
							//{
							//    MiddleName = row["MI"].ToString();
							//}
							//if (dSet.Tables[0].Columns.IndexOf("SUFFIX") > 0)
							//{
							//    Suffix = row["SUFFIX"].ToString();
							//}
							//if (dSet.Tables[0].Columns.IndexOf("ZIP4") > 0)
							//{
							//    Zip4 = row["ZIP4"].ToString();
							//}
							//rTB["MiddleName"] = MiddleName;
							//rTB["Suffix"] = Suffix;
							//rTB["Zip"] = row["ZIP"].ToString() + Zip4;

							tbMember.Rows.Add(rTB);
						}
						
						count = db.ExecuteSP("xp_member_upload",
									new string[] { "tbMem", "CountISN", "updatedBy" },
									new object[] { tbMember, CountISN, this.User.UserISN
												});
						if (count < 0)
						{
							result = "2";                                    
						}
						else if(count > 0)
						{
							db.ExecuteStoredProc("xp_datacount_insupd",
							new string[] { "CountISN", "DataReceivedFiles", "updatedBy" },
							new object[] { CountISN, fileNameReceived, this.User.UserISN }, false);                                    
						}
					}                                
				}
				catch (Exception ex)
				{
					result = "3";
					
				}
			}                    
		}
		else
		{
			result = "4";                    
		}
	}
	return result;
}

Database
USE [PremierDataWarehouse]
GO

/****** Object:  StoredProcedure [dbo].[xp_member_upload]    Script Date: 09/21/17 4:27:48 PM ******/
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



		
filter điều kiện khi load xtable, nút SaveView

protected void xTable_InitXTableEvent(object sender, WebControlLibrary.XTable.Event.InitXTableArgs args)
{
	if (!IsPostBack)
	{
		SearchCell scell = (SearchCell)this.xTable.SearchRow.SearchCells["Push"];
		scell.SearchValues = new object[] { 1 };
		xTable.ApplySearchValues();
		this.XTable_LoadState(this.xTable);
	}
}
oninitxtableevent="xTable_InitXTableEvent"
dlg_closeClick(this);

Thêm nút Save View vào page bất kì

protected void xTable_CommandEvent(object sender, WebControlLibrary.XTable.Event.CommandArgs args)
{
	switch (args.Command.Name)
	{
		case "btnSaveView":
			int rs = this.XTable_SaveState(args.XTable);
			if (rs < 0)
			{
				this.ShowErrorTop(lrError, "sys_Err");
			}
			break;                
	}
}		
 protected void xTable_InitXTableEvent(object sender, WebControlLibrary.XTable.Event.InitXTableArgs args)
{
	if (!IsPostBack)
	{
		SearchCell scell = (SearchCell)this.xTable.SearchRow.SearchCells["cdtIsPush"];
		scell.SearchValues = new object[] { 1 };
		xTable.ApplySearchValues();
	}            
	this.XTable_LoadState(this.xTable);
}
<xx:Command Width="" Text="Save View" Name="btnSaveView" CommandType="PostBack"></xx:Command>
oncommandevent="xTable_CommandEvent"
oninitxtableevent="xTable_InitXTableEvent"

Load dữ liệu vào dropdownlist 
<asp:DropDownList ID="ddlPackageValue" runat="server" Width="170px" Height="25px">                
            </asp:DropDownList>
public void loadPackageOneTop()
{
	for (int i = 0; i < 19; i++)
	{
		ddlPackageValue.Items.Insert(i, new ListItem(Functions.ConvertObjectToDecimal(i*1000).ToString("#,##0"), i.ToString()));
	}
	
}
		
var dSetMaxAmount = this.db.ExecuteQuery("select MaxAmount from Vw_RateSetting  order by MaxAmount asc");                
int i = 0;
foreach (DataRow row in dSetMaxAmount.Tables[0].Rows)
{
	if (!string.IsNullOrEmpty(row["MaxAmount"].ToString()) && Functions.ConvertObjectToInt(row["MaxAmount"]) != 0)
	{
		ddlPackageValue.Items.Insert(i, new ListItem(Functions.ConvertObjectToDecimal(row["MaxAmount"]).ToString("#,##0"), i.ToString()));
		i++;
	}
}

Load tự động khi nhập change field
<asp:TextBox ID="txtMaxValue1" runat="server"  CssClass="RightNumber" Width="140px" onblur="reloadValue();" onpaste="this.onblur();" onkeyup="this.onblur();"></asp:TextBox>
function reloadValue()
{
	var MaxValue1 = $("[id$='txtMaxValue1']").val();               
	var MinValue2 = $("[id$='txtMinValue2']").val(MaxValue1);       
}

$(document).ready(function () {
	reloadValue();
});	

style="vertical-align: inherit;" canh giữa

SelectIndex dropdownlist bằng jquery
var ddlValue = $("[id$='ddlPackageValue']").val();        
var selectedDropDown = $("[id$='ddlPackageValue']").prop('selectedIndex', ddlValue);
SelectIndex dropdownlist bằng c#
ddlPackageValue.SelectedValue = rowValue["Value"].ToString();

Xóa hết dấu phảy bằng js
number.replace(/\,/g, '') % 1000;
Giá trị tương đối sử dụng float, double
Giá trị chính xác sử dụng decimal

Load configtemplate
public static DataSet LoadConfigTemplate(string pathFull)
{
	var dsConfig = new DataSet();
	try
	{
		dsConfig.ReadXml(pathFull);
		return dsConfig;
	}
	catch (Exception ex)
	{
	}
	return dsConfig;
}
var dsRecipient = Common.HtmlHelper.LoadConfigTemplate(HttpContext.Current.Server.MapPath("~/XML/RecipentBureau.xml"));

Thay đổi src của iframe

 jQuery('iframe[src*="https://www.youtube.com/embed/"]').addClass("youtube-iframe");

// changes the iframe src to prevent playback or stop the video playback in our case
$('.youtube-iframe').each(function(index) {
	$(this).attr('src', $(this).attr('src'));
	return false;
});

Con trỏ bàn tay style='cursor:pointer;'
 
Thanh tiến trình progress bar

Status change to:
 Document Tracking
 DOCUMENT TRACKING
      Assign Document Processor
	  Processor
	  ASSIGN DOCUMENT(S) TO DOCUMENT PROCESSOR 39543
	  select m.memberISN, m.Username as memUsername, (memFirstName + ' ' + memLastName) as memFullName2, m1.memPhone, m1.memFirstName, m1.memLastName from MemberExt2 m left join Member  m1 on m1.memberISN = m.memberISN where m1.memStatus=1 and m.DealerISN=39543 and memSSN='8888'
	  Doc Processor	  
	  
	  Get New Document
	  Database.ExecuteStoredProc("xp_docprocessor_getnewdocument",new string[] { "DealerISN", "ProcessorISN" },new object[] { this.UserDealerISN, this.UserISN });
	  
Thêm filed có dạng danh sách vào xtable	  

<xx:Column Width="100px" DataType="String" DisplayName="Status" FieldName="docProcessorStatus"
	ColumnName="docType" Sortable="True" ItemControl="Custom" SearchType="ListItem" HorizontalAlign="Center">
	<Attributes>
		<xx:Attr Key="ListItems" Value="../Xml/DocumentStatus.xml"></xx:Attr>
	</Attributes>
</xx:Column>

public DataSet DocumentStatus
	{
		get
		{
			DataSet docStatus = new DataSet();
			docStatus.ReadXml(Server.MapPath("~/XML/DocumentStatus.xml"));
			return docStatus;
		}
	}

public string changeStatusFromDB(DataSet ds, int status)
{
	if (!Common.HtmlHelper.IsEmptyDataSet(ds))
	{
		foreach (DataRow row in ds.Tables[0].Rows)
		{
			if (ConvertObjectToInt(row["value"], 0) == status)
			{
				return row["text"].ToString();
			}
		}
	}
	return string.Empty;
}

Thêm column, thay đổi tên column		
public void getFileFrom_DataCount(int CountISN, string pathFile, int Type)
{            
	try
	{
		if (Type == 1)
		{                    
			DataSet ds = db.ExecuteQuery("select STATE, Debt1Qty, Debt2Qty from DataOrderDetail where CountISN=" + CountISN);

			DataSet dsTotal = db.ExecuteQuery("select Sum(Debt1Qty) as TotalDebt1Qty, Sum(Debt2Qty) as TotalDebt2Qty from DataOrderDetail where CountISN=" + CountISN);

			decimal totalDebt1Qty = Functions.ConvertObjectToInt(dsTotal.Tables[0].Rows[0]["TotalDebt1Qty"]);
			decimal totalDebt2Qty = Functions.ConvertObjectToInt(dsTotal.Tables[0].Rows[0]["TotalDebt2Qty"]);
			
			if (ds != null)
			{
				DataTable tbTemp = new DataTable();
				tbTemp.Columns.Add("STATE");
				tbTemp.Columns.Add("9-19#0");
				tbTemp.Columns.Add("%");
				tbTemp.Columns.Add("19-32#5");
				tbTemp.Columns.Add("%2");

				foreach (DataRow row in ds.Tables[0].Rows)
				{
					DataRow rTB = tbTemp.NewRow();
					rTB["STATE"] = row["STATE"];
					rTB["9-19#0"] = row["Debt1Qty"];
					rTB["%"] = Functions.ConvertObjectToDecimal(row["Debt1Qty"])*100/totalDebt1Qty + "%";
					rTB["19-32#5"] = row["Debt2Qty"];
					rTB["%2"] = Functions.ConvertObjectToDecimal(row["Debt2Qty"])*100/ totalDebt2Qty + "%";
					tbTemp.Rows.Add(rTB);
				}
				//ds.Row
				//DataTable dTable = ds.Tables[0];
				//tbTemp.Columns["Debt1Qty"].ColumnName = "9-19.0";
				//tbTemp.Columns["Debt2Qty"].ColumnName = "19-32.5";
				tbTemp.Rows.Add(new object[] { "Total", totalDebt1Qty, totalDebt2Qty });
				tbTemp.AcceptChanges();
				Functions.CreateCSVFile(pathFile, tbTemp);
			}
		}     
											
	}
	catch (Exception)
	{

	}

}

IDDialogTaskToDo
var frame = window.frames[name];
if(frame.length > 0){
	frame.frameElement.contentWindow.OnLoadDataCreditor();
}else{
	window.frams["IDDialogTaskToDo"].frames[name].frameElement.contentWindow.OnLoadDataCreditor();
}

var frame = window.frames[name];
if(frame){
	frame.frameElement.contentWindow.OnLoadDataCreditor();
}else{
	window.frames["IDDialogTaskToDo"].frames[name].frameElement.contentWindow.OnLoadDataCreditor();
}			

Reload iframe
var iframe = document.getElementById('frameView');
iframe.src = iframe.src;

Gắn css cho đối tượng, thêm transform cho đối tượng
$('iframe').css({
	'-webkit-transform': 'rotate(' + changeCorner + 'deg)',
	'-moz-transform': 'rotate(' + changeCorner + 'deg)',
	'-ms-transform': 'rotate(' + changeCorner + 'deg)',
	'-o-transform': 'rotate(' + changeCorner + 'deg)',
	'transform': 'rotate(' + changeCorner + 'deg)'
});

Ajax upload file.
Code ajax javascript
function UpFile() {
	//debugger;
	var uploadFiles = document.getElementById('<%=uploadFiles.ClientID%>');
	var fileName = '';
	if (uploadFiles.files[0]) {
		fileName = uploadFiles.files[0].name;
	}

	if (fileName != "") {
		var dataForm = new FormData();
		dataForm.append('fn', 'UploadDocument');
		dataForm.append('file', uploadFiles.files[0]);

		$.ajax({
			type: "POST",
			url: "AjaxHandlers/AjaxAction.ashx",
			data: dataForm,
			contentType: false,
			processData: false,
			success: function(result) {
				//debugger;
				$('#showButton').hide();
				if (fileName.indexOf(".png") > -1 || fileName.indexOf(".jpg") > -1 || fileName.indexOf(".html") > -1) {

					$("#frameView").attr('src', result);
					if (fileName.indexOf(".png") > -1 || fileName.indexOf(".jpg") > -1) {
						$('#showButton').show();
					}
				}
				else if (fileName.indexOf(".pdf") > -1 || fileName.indexOf(".xlsx") > -1 || fileName.indexOf(".xls") > -1) {
					var viewer = "https://docs.google.com/gview?url=" + result + "&embedded=true";
					$("#frameView").attr('src', viewer);
				}
			},
			error: function(xhr, ajaxOptions, thrownError) {
				alert("System Error");
			}
		});
	}
}
	
Code C# backend
public string UploadDocument(HttpPostedFile fileUpload)
{
	string sRs = "";
	string saveTo = Path.Combine(System.Configuration.ConfigurationSettings.AppSettings["SaveFilesDir"], "Temp");
	if (!Directory.Exists(saveTo)) Directory.CreateDirectory(saveTo);
	var sFullFileName = Path.Combine(saveTo, fileUpload.FileName);
	fileUpload.SaveAs(sFullFileName);

	string sFolderURL = Path.Combine(System.Configuration.ConfigurationSettings.AppSettings["SaveFolderURL"], "Temp");
	string sFileURL = Path.Combine(sFolderURL, HttpUtility.UrlEncode(fileUpload.FileName).Replace("+","%20"));

	sRs = sFileURL.Replace("\\", "/");

	return sRs;
}
	
Export file Excel
public void ExportAll(WebControlLibrary.XTable.XTable xTable, string wordSheetName, string fileName)
{
	using (ExcelPackage excelPackage = new ExcelPackage())
	{
		ExcelWorksheet excelWorkSheet = excelPackage.Workbook.Worksheets.Add(wordSheetName);
		var listFieldName = new List<string>();
		var countRow = 3;

		excelWorkSheet.Cells[countRow, 1].Value = "No.";
		excelWorkSheet.Cells[countRow, 1].AutoFitColumns();
		excelWorkSheet.Cells[countRow, 1].Style.Font.Bold = true;

		//Header of table for Excel Report
		for (int i = 0; i < xTable.Columns.Count; i++)
		{
			var sName = xTable.Columns[i].DisplayName;
			var format = xTable.Columns[i].Format;
			listFieldName.Add(xTable.Columns[i].FieldName);
			if (!string.IsNullOrEmpty(sName))
			{
				excelWorkSheet.Cells[countRow, i + 2].Value = sName;
				excelWorkSheet.Cells[countRow, i + 2].AutoFitColumns();
			}
			//Format text for file Excel Report
			using (ExcelRange col = excelWorkSheet.Cells[countRow, i + 2])
			{
				col.Style.Font.Bold = true;
			}
		}
		excelWorkSheet.Column(11).Width = 11;//Width of final column for Excel Report
		//Header of file Excel Report
		Common.EPPlusHelper.FormatExcelString(excelWorkSheet, 1, 2, "From Date:", ExcelAlignment.Right);
		excelWorkSheet.Cells[1, 2].Style.Font.Bold = true;
		Common.EPPlusHelper.FormatExcelString(excelWorkSheet, 1, 3, this.dtxtFrom.Text, ExcelAlignment.Left);
		Common.EPPlusHelper.FormatExcelString(excelWorkSheet, 1, 5, "To Date:", ExcelAlignment.Right);
		excelWorkSheet.Cells[1, 5].Style.Font.Bold = true;
		Common.EPPlusHelper.FormatExcelString(excelWorkSheet, 1, 6, this.dtxtTo.Text, ExcelAlignment.Left);
		//Common.EPPlusHelper.FormatExcelString(excelWorkSheet, 1, 8, "Team Manager:", ExcelAlignment.Right);
		//excelWorkSheet.Cells[1, 8].Style.Font.Bold = true;
		//Common.EPPlusHelper.FormatExcelString(excelWorkSheet, 1, 9, this.ddlTeamManager.SelectedItem.Text.Replace(" -", "").Replace("- ", "").Replace("-", ""), ExcelAlignment.Left);

		listFieldName.Insert(0, "Index");
		int itemIndex = 1;
		int TotalNew = 0;
		int TotalWaitforAM = 0;
		int TotalReject = 0;
		int TotalApproved = 0;
		decimal ApprovedRate = 0;

		//Get full data from dataSet
		//string teamManagerISN = this.ddlTeamManager.SelectedValue;
		//DataSet dSet = null;
		//if (string.IsNullOrEmpty(teamManagerISN))
		//{
		//    dSet = Database.ExecuteStoredProcReturnDataSet("xp_debtext_report_amdashboard", new string[] { "DealerISN", "Since", "Until", "TeamManagerISN" }, new object[] { this.UserDealerISN, this.dtxtFrom.Text, this.dtxtTo.Text, DBNull.Value });
		//}
		//else
		//{
		//    dSet = Database.ExecuteStoredProcReturnDataSet("xp_debtext_report_amdashboard", new string[] { "DealerISN", "Since", "Until", "TeamManagerISN" }, new object[] { this.UserDealerISN, this.dtxtFrom.Text, this.dtxtTo.Text, teamManagerISN });
		//}

		DataSet dSet = null;
		dSet = Database.ExecuteStoredProcReturnDataSet("xp_document_processor_dashboard_report", new string[] { "DealerISN", "Since", "Until" }, new object[] { this.UserDealerISN, this.dtxtFrom.Text, this.dtxtTo.Text });

		if (!Common.HtmlHelper.IsEmptyDataSet(dSet))
		{
			foreach (DataRow iRow in dSet.Tables[0].Rows)
			{
				countRow++;
				var iColumn = 0;
				foreach (var fName in listFieldName)
				{
					var sInfo = string.Empty;
					iColumn++;
					switch (fName)
					{
						case "Index":
							//Common.EPPlusHelper.FormatExcelString(ws, countRow, iColumn, itemIndex.ToString(), ExcelAlignment.Left);
							Common.EPPlusHelper.FormatExcelNumber(excelWorkSheet, countRow, iColumn, itemIndex);
							excelWorkSheet.Cells[countRow, iColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
							continue;
						case "memUserName":
							var fullname = iRow["memFirstName"].ToString() + " " + iRow["memLastName"].ToString();
							var userName = iRow["memUserName"].ToString();
							if (string.IsNullOrEmpty(fullname))
							{
								sInfo = userName;
							}
							else
							{
								sInfo = userName + " (" + fullname + ")";
							}
							Common.EPPlusHelper.FormatExcelString(excelWorkSheet, countRow, iColumn, sInfo, ExcelAlignment.Left);
							continue;
						case "TotalNew":
							TotalNew += ConvertObjectToInt(iRow[fName]);
							break;
						case "TotalWaitforAM":
							TotalWaitforAM += ConvertObjectToInt(iRow[fName]);
							break;
						case "TotalReject":
							TotalReject += ConvertObjectToInt(iRow[fName]);
							break;
						case "TotalApproved":
							TotalApproved += ConvertObjectToInt(iRow[fName]);
							break;
						case "ApprovedRate":
							string strApprovedRate = iRow["ApprovedRate"].ToString();
							if (!string.IsNullOrEmpty(strApprovedRate))
							{
								strApprovedRate = (ConvertObjectToDecimal(strApprovedRate) / 100).ToString();
								Common.EPPlusHelper.FormatExcelPercent(excelWorkSheet, countRow, iColumn, strApprovedRate);
							}
							else
							{
								sInfo = "N/A";
								Common.EPPlusHelper.FormatExcelString(excelWorkSheet, countRow, iColumn, sInfo, ExcelAlignment.Right);
							}
							continue;
						default:
							break;
					}
					Common.EPPlusHelper.FormatExcelNumber(excelWorkSheet, countRow, iColumn, iRow[fName]);
				}
				itemIndex++;
			}
			var iColumnTotal = 0;
			countRow++;
			ApprovedRate = TotalApproved + TotalReject;
			if (ApprovedRate > 0)
			{
				ApprovedRate = TotalApproved / ApprovedRate;
			}
			foreach (var fName in listFieldName)
			{
				iColumnTotal++;
				switch (fName)
				{
					case "memUserName":
						Common.EPPlusHelper.FormatExcelString(excelWorkSheet, countRow + 1, iColumnTotal, "Total", ExcelAlignment.Right);
						break;
					case "TotalNew":
						Common.EPPlusHelper.FormatExcelNumber(excelWorkSheet, countRow + 1, iColumnTotal, TotalNew);
						break;
					case "TotalWaitforAM":
						Common.EPPlusHelper.FormatExcelNumber(excelWorkSheet, countRow + 1, iColumnTotal, TotalWaitforAM);
						break;
					case "TotalReject":
						Common.EPPlusHelper.FormatExcelNumber(excelWorkSheet, countRow + 1, iColumnTotal, TotalReject);
						break;
					case "TotalApproved":
						Common.EPPlusHelper.FormatExcelNumber(excelWorkSheet, countRow + 1, iColumnTotal, TotalApproved);
						break;
					case "ApprovedRate":
						Common.EPPlusHelper.FormatExcelPercent(excelWorkSheet, countRow + 1, iColumnTotal, ApprovedRate);
						break;
					default:
						break;
				}
				using (ExcelRange col = excelWorkSheet.Cells[countRow + 1, iColumnTotal])
				{
					col.Style.Font.Bold = true;
				}
			}
			HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
			HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + ".xlsx");
			HttpContext.Current.Response.BinaryWrite(excelPackage.GetAsByteArray());
			HttpContext.Current.Response.End();
		}
	}
}

 public void ExportExcel(WebControlLibrary.XTable.XTable xTable, string wordSheetName, string fileName)
{
	using (ExcelPackage excelPackage = new ExcelPackage())
	{
		ExcelWorksheet excelWorkSheet = excelPackage.Workbook.Worksheets.Add(wordSheetName);
		var listFieldName = new List<string>();
		var countRow = 1;
		var totalRefreshDeals = 0;
		var totalRefreshLeads = 0;
		var totalRenewDeals = 0;
		var totalRenewLeads = 0;
		var totalOutbound = 0;
		var totalSalesIssue = 0;
		var totalDeals = 0;                

		decimal totalSoldDebt = 0;
		decimal totalActiveDebt = 0;
		decimal totalDuration = 0;

		var totalCancelled1st = 0;
		var totalCancelled1st3rd = 0;
		var totalCancelled4th9th = 0;
		var totalCancelled10th15th = 0;
		var totalCancelled16th = 0;

		for (int i = 0; i < xTable.Columns.Count; i++)
		{
			var sName = xTable.Columns[i].DisplayName;
			var format = xTable.Columns[i].Format;
			listFieldName.Add(xTable.Columns[i].FieldName);
			if (!string.IsNullOrEmpty(sName))
			{
				excelWorkSheet.Cells[countRow, i + 1].Value = sName;
				excelWorkSheet.Cells[countRow, i + 1].AutoFitColumns();
			}
			using (ExcelRange column = excelWorkSheet.Cells[countRow, i + 1])
			{
				column.Style.Font.Bold = true;
			}
			if (i == 0)
			{
				excelWorkSheet.Column(i + 1).Width = 40;//width of first column
			}
			//if (i == 6)
			//{
			//    excelWorkSheet.Column(i + 1).Width = 15;
			//}
			//if (i == 7)
			//{
			//    excelWorkSheet.Column(i + 1).Width = 15;
			//}
			//if (i == 8)
			//{
			//    excelWorkSheet.Column(i + 1).Width = 15;
			//}
		}
		foreach (WebControlLibrary.XTable.Rows.ItemRow iRow in xTable.ItemRows)
		{
			countRow++;
			var iColumn = 1;
			foreach (var fName in listFieldName)
			{
				var sInfo = string.Empty;
				#region get sInfo Process
				if (fName.ToLower() == "UserName".ToLower())
				{
					var fullname = iRow.RowData["memFirstName"].ToString() + " " + iRow.RowData["memLastName"].ToString();
					var userName = iRow.RowData["UserName"].ToString();
					if (string.IsNullOrEmpty(fullname))
					{
						sInfo = userName;
					}
					else
					{
						sInfo = userName + " (" + fullname + ")";
					}
					Common.EPPlusHelper.FormatExcelString(excelWorkSheet, countRow, iColumn, sInfo, ExcelAlignment.Left);
					//ws.Cells[countRow, iColumn].Value = sInfo;
				}
				else if (fName.ToLower() == "RefreshClosing".ToLower())
				{
					if (iRow.RowData["RefreshClosing"].ToString() != string.Empty)
					{
						var refreshClosing = Math.Round(StringUtil.StringToDecimal(iRow.RowData["RefreshClosing"].ToString(), 0), MidpointRounding.ToEven);
						Common.EPPlusHelper.FormatExcelPercent(excelWorkSheet, countRow, iColumn, refreshClosing / 100);
					}
					else
					{
						sInfo = "N/A";
						Common.EPPlusHelper.FormatExcelString(excelWorkSheet, countRow, iColumn, sInfo, ExcelAlignment.Right);
					}                          
				}
				else if (fName.ToLower() == "RenewClosing".ToLower())
				{
					if (iRow.RowData["RenewClosing"].ToString() != string.Empty)
					{
						var renewClosing = Math.Round(StringUtil.StringToDecimal(iRow.RowData["RenewClosing"].ToString(), 0), MidpointRounding.ToEven);
						Common.EPPlusHelper.FormatExcelPercent(excelWorkSheet, countRow, iColumn, renewClosing / 100);
					}
					else
					{
						sInfo = "N/A";
						Common.EPPlusHelper.FormatExcelString(excelWorkSheet, countRow, iColumn, sInfo, ExcelAlignment.Right);
					}
				}
				else if (fName.ToLower() == "Closing".ToLower())
				{
					if (iRow.RowData["Closing"].ToString() != string.Empty)
					{
						var renewClosing = Math.Round(StringUtil.StringToDecimal(iRow.RowData["Closing"].ToString(), 0), MidpointRounding.ToEven);
						Common.EPPlusHelper.FormatExcelPercent(excelWorkSheet, countRow, iColumn, renewClosing / 100);
					}
					else
					{
						sInfo = "N/A";
						Common.EPPlusHelper.FormatExcelString(excelWorkSheet, countRow, iColumn, sInfo, ExcelAlignment.Right);
					}
				}

				else if (fName.ToLower() == "RefreshDeals".ToLower() || fName.ToLower() == "RefreshLeads".ToLower() || fName.ToLower() == "RenewDeals".ToLower() || fName.ToLower() == "RenewLeads".ToLower() ||
					fName.ToLower() == "SalesIssue".ToLower() || fName.ToLower() == "Outbound".ToLower() || fName.ToLower() == "Cancelled1st".ToLower() || fName.ToLower() == "Cancelled1st3rd".ToLower() || fName.ToLower() == "Cancelled4th9th".ToLower()
					 || fName.ToLower() == "Cancelled10th15th".ToLower() || fName.ToLower() == "Cancelled16th".ToLower() || fName.ToLower() == "Deals".ToLower())
				{
					try
					{
						sInfo = iRow.RowData[fName].ToString();
						if (fName.ToLower() == "RefreshDeals".ToLower())
						{
							totalRefreshDeals += ConvertObjectToInt(sInfo, 0);                                    
						}
						else if (fName.ToLower() == "RefreshLeads".ToLower())
						{
							totalRefreshLeads += ConvertObjectToInt(sInfo, 0);
						}
						else if (fName.ToLower() == "RenewDeals".ToLower())
						{
							totalRenewDeals += ConvertObjectToInt(sInfo, 0);
						}
						else if (fName.ToLower() == "RenewLeads".ToLower())
						{
							totalRenewLeads += ConvertObjectToInt(sInfo, 0);
						}
						else if (fName.ToLower() == "SalesIssue".ToLower())
						{
							totalSalesIssue += ConvertObjectToInt(sInfo, 0);                                    
						}
						else if (fName.ToLower() == "Outbound".ToLower())
						{
							totalOutbound += ConvertObjectToInt(sInfo, 0);
						}
						else if (fName.ToLower() == "Cancelled1st".ToLower())
						{
							totalCancelled1st += ConvertObjectToInt(sInfo, 0);
						}
						else if (fName.ToLower() == "Cancelled1st3rd".ToLower())
						{
							totalCancelled1st3rd += ConvertObjectToInt(sInfo, 0);
						}
						else if (fName.ToLower() == "Cancelled4th9th".ToLower())
						{
							totalCancelled4th9th += ConvertObjectToInt(sInfo, 0);
						}
						else if (fName.ToLower() == "Cancelled10th15th".ToLower())
						{
							totalCancelled10th15th += ConvertObjectToInt(sInfo, 0);
						}
						else if (fName.ToLower() == "Cancelled16th".ToLower())
						{
							totalCancelled16th += ConvertObjectToInt(sInfo, 0);
						}
						else if (fName.ToLower() == "Deals".ToLower())
						{
							totalDeals += ConvertObjectToInt(sInfo, 0);
						}
						Common.EPPlusHelper.FormatExcelNumber(excelWorkSheet, countRow, iColumn, sInfo);
					}
					catch (Exception) { }
				}
				else if (fName.ToLower() == "TotalSoldDebt".ToLower() || fName.ToLower() == "TotalActiveDebt".ToLower())
				{
					try
					{
						sInfo = iRow.RowData[fName].ToString();                               
						if (fName.ToLower() == "TotalSoldDebt".ToLower())
						{
							totalSoldDebt += ConvertObjectToDecimal(sInfo, 0);                                    
						}
						else if (fName.ToLower() == "TotalActiveDebt".ToLower())
						{
							totalActiveDebt += ConvertObjectToDecimal(sInfo, 0);
						}
						Common.EPPlusHelper.FormatExcelMoney(excelWorkSheet, countRow, iColumn, sInfo);    
					}
					catch (Exception) { }
				}                        
				else if (fName.ToLower() == "Duration".ToLower())
				{
					try
					{
						int talkTime = ConvertObjectToInt(iRow.RowData[fName]);
						totalDuration += talkTime;
						Common.EPPlusHelper.FormatExcelString(excelWorkSheet, countRow, iColumn, string.Format("{0}h:{1:00}m", talkTime / 3600, (talkTime % 3600) / 60), ExcelAlignment.Right);
					}
					catch (Exception) { }
				}                                             
				else
				{
					Common.EPPlusHelper.FormatExcelString(excelWorkSheet, countRow, iColumn, sInfo, ExcelAlignment.Left);
					
				}
				#endregion
				iColumn++;
				
			}                    
		}
		//total
		var iColumnTotal = 0;
		foreach (var fName in listFieldName)
		{
			iColumnTotal++;
			if (fName.ToLower() == "UserName".ToLower())
			{
				Common.EPPlusHelper.FormatExcelString(excelWorkSheet, countRow + 1, iColumnTotal, "Sub Total", ExcelAlignment.Right);
			}
			else if (fName.ToLower() == "Deals".ToLower())
			{
				//totalDeals = totalRefreshDeals + totalRenewDeals;
				Common.EPPlusHelper.FormatExcelNumber(excelWorkSheet, countRow + 1, iColumnTotal, totalDeals);
			}
			else if (fName.ToLower() == "RefreshClosing".ToLower())
			{
				if (totalRefreshLeads > 0)
				{
					decimal refreshClosing = Math.Round(ConvertObjectToDecimal(totalRefreshDeals, 0) / ConvertObjectToDecimal(totalRefreshLeads, 0), 2);
					Common.EPPlusHelper.FormatExcelPercent(excelWorkSheet, countRow + 1, iColumnTotal, refreshClosing);
				}
				else
				{
					Common.EPPlusHelper.FormatExcelString(excelWorkSheet, countRow + 1, iColumnTotal, "N/A", ExcelAlignment.Right);
				}
			}
			else if (fName.ToLower() == "RenewClosing".ToLower())
			{
				if (totalRenewLeads > 0)
				{
					//decimal closing = Math.Round(ConvertObjectToDecimal(totalDeals, 0) / ConvertObjectToDecimal(totalPrimaryLead, 0), 2);
					decimal renewClosing = Math.Round(ConvertObjectToDecimal(totalRenewDeals, 0) / ConvertObjectToDecimal(totalRenewLeads, 0), 2);
					Common.EPPlusHelper.FormatExcelPercent(excelWorkSheet, countRow + 1, iColumnTotal, renewClosing);
				}
				else
				{
					Common.EPPlusHelper.FormatExcelString(excelWorkSheet, countRow + 1, iColumnTotal, "N/A", ExcelAlignment.Right);
				}
			}
			else if (fName.ToLower() == "Closing".ToLower())
			{
				if (totalRefreshLeads > 0)
				{
					//decimal closing = Math.Round(ConvertObjectToDecimal(totalDeals, 0) / ConvertObjectToDecimal(totalPrimaryLead, 0), 2);
					decimal Closing = Math.Round(ConvertObjectToDecimal(totalDeals, 0) / ConvertObjectToDecimal(totalRefreshLeads, 0), 2);
					Common.EPPlusHelper.FormatExcelPercent(excelWorkSheet, countRow + 1, iColumnTotal, Closing);
				}
				else
				{
					Common.EPPlusHelper.FormatExcelString(excelWorkSheet, countRow + 1, iColumnTotal, "N/A", ExcelAlignment.Right);
				}
			}
			else if (fName.ToLower() == "RefreshDeals".ToLower())
			{
				Common.EPPlusHelper.FormatExcelNumber(excelWorkSheet, countRow + 1, iColumnTotal, totalRefreshDeals);
			}
			else if (fName.ToLower() == "RefreshLeads".ToLower())
			{
				Common.EPPlusHelper.FormatExcelNumber(excelWorkSheet, countRow + 1, iColumnTotal, totalRefreshLeads);
			}
			else if (fName.ToLower() == "RenewDeals".ToLower())
			{
				Common.EPPlusHelper.FormatExcelNumber(excelWorkSheet, countRow + 1, iColumnTotal, totalRenewDeals);
			}
			else if (fName.ToLower() == "RenewLeads".ToLower())
			{
				Common.EPPlusHelper.FormatExcelNumber(excelWorkSheet, countRow + 1, iColumnTotal, totalRenewLeads);
			}
			else if (fName.ToLower() == "Outbound".ToLower())
			{
				Common.EPPlusHelper.FormatExcelNumber(excelWorkSheet, countRow + 1, iColumnTotal, totalOutbound);
			}
			else if (fName.ToLower() == "SalesIssue".ToLower())
			{
				Common.EPPlusHelper.FormatExcelNumber(excelWorkSheet, countRow + 1, iColumnTotal, totalSalesIssue);
			}
			else if (fName.ToLower() == "Cancelled1st".ToLower())
			{
				Common.EPPlusHelper.FormatExcelNumber(excelWorkSheet, countRow + 1, iColumnTotal, totalCancelled1st);
			}
			else if (fName.ToLower() == "Cancelled1st3rd".ToLower())
			{
				Common.EPPlusHelper.FormatExcelNumber(excelWorkSheet, countRow + 1, iColumnTotal, totalCancelled1st3rd);
			}
			else if (fName.ToLower() == "Cancelled4th9th".ToLower())
			{
				Common.EPPlusHelper.FormatExcelNumber(excelWorkSheet, countRow + 1, iColumnTotal, totalCancelled4th9th);
			}
			else if (fName.ToLower() == "Cancelled10th15th".ToLower())
			{
				Common.EPPlusHelper.FormatExcelNumber(excelWorkSheet, countRow + 1, iColumnTotal, totalCancelled10th15th);
			}
			else if (fName.ToLower() == "Cancelled16th".ToLower())
			{
				Common.EPPlusHelper.FormatExcelNumber(excelWorkSheet, countRow + 1, iColumnTotal, totalCancelled16th);
			}
			else if (fName.ToLower() == "TotalSoldDebt".ToLower())
			{
				Common.EPPlusHelper.FormatExcelMoney(excelWorkSheet, countRow + 1, iColumnTotal, totalSoldDebt);
			}
			else if (fName.ToLower() == "TotalActiveDebt".ToLower())
			{
				Common.EPPlusHelper.FormatExcelMoney(excelWorkSheet, countRow + 1, iColumnTotal, totalActiveDebt);
			}                    
			else if (fName.ToLower() == "Duration".ToLower())
			{
				Common.EPPlusHelper.FormatExcelString(excelWorkSheet, countRow + 1, iColumnTotal, string.Format("{0}h:{1:00}m", Math.Floor(totalDuration / 3600), Math.Floor((totalDuration % 3600) / 60)), ExcelAlignment.Right);
			}                                                 
			using (ExcelRange column = excelWorkSheet.Cells[countRow + 1, iColumnTotal])
			{
				column.Style.Font.Bold = true;
			}
		}

		HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
		HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + ".xlsx");
		HttpContext.Current.Response.BinaryWrite(excelPackage.GetAsByteArray());
		HttpContext.Current.Response.End();
	}
}
		
Kiểm tra Department khi login 
UserDepartmentCode.Equals("1188")

Disable input button
$("[id$='btnDelete']").attr('disabled', 'disabled');

Bỏ disabled input button
$("[id$='btnDelete']").removeAttr("disabled"); 

Hide button theo department
$(document).ready(function() {
	var department = '<%=UserDepartmentCode %>';
	if (department == '5555') {
		$("[id$='btnDelete']").attr('disabled', 'disabled');
	}        
});

$("[id$='btnCaculate']").attr("disabled", false);

if (vMail_Total === 100)
{
	$("[id$='btnCaculate']").attr("disabled", false);
}
else
	$("[id$='btnCaculate']").attr("disabled", true);


chỉ cho phép nhập number
$("[id$='txtMaxValue" + i + "']").keypress(function (e) {                
	if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {                    
		return false;
	}
});
xóa remove dấu phẩy(commas)
.replace(/,/g, "");
.replace(",", "");
cho phép nhập number có dấu chấm
$("[id$='txtLeadPortfolio" + i + "']").keypress(function (e) {
	if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
		//display error message
		//$("#errmsg").html("Digits Only").show().fadeOut("slow");
		if (e.which == 46) {
			$(this).val(this.val() + '.');
			e.preventDefault();
		}
		else {
			return false;
		}
		//return false;
	}
});		
	
thêm link cho cell 
cell.Text = string.Format("<a href='{0}' class='LinkItem'>{1}</a>", linkDocumentISN + "&DocumentISN=" + args.RowData["DocumentISN"], args.RowData["DocumentISN"].ToString());
cell.Text = "<a href='javascript:' class='LinkItem' onclick=\"location.href='Lead_Main.aspx?isn=" + args.RowData["MemberISN"] + "&p=document&docISN=" + args.RowData["DocumentISN"].ToString() + "&down=1'\" >(Pending for contract sign)</a>";

truyền tham số bằng Session

if (Request["CountISN"] != null && Request["CountISN"] != string.Empty)
{
	HttpContext.Current.Session["CountISN"] = Request["CountISN"];
}
sort sql
ORDER BY namecolumn ASC|DESC;
select top 5 *  from CalculateHistory order by updatedDate asc
select top 1 a.CountISN as CountISN, b.TotalDebt1Qty as TotalDebt1Qty, b.TotalDebt2Qty TotalDebt2Qty, b.TotalQty as TotalQty  from CalculateHistory as a, Vw_DataCount as b where a.CountISN=b.CountISN order by a.updatedDate desc

sp_help xp_documentdistributionsetting_upd
exec xp_documentdistributionsetting_upd
sp_helptext xp_debtext_document_mgmt_getpage
sp_helptext xp_documentdistributionsetting_upd

use &amp; in place of &

Kiểm tra Dataset có null hay không, 
 public static bool IsEmptyDataSet(DataSet ds)
{
	if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) return false;
	return true;
}

Typeof trong c#
Phân biệt giữa typeof, GetType và is https://lvluat.wordpress.com/2013/05/01/c-phan-biet-giua-typeof-gettype-va-is/
+ typeof trả về kiểu của một lớp
System.Type  typeof(TênLớp)
trong đó TênLớp là tên một lớp đã khai báo. TênLớp được xác định khi thiết kế (design time).
+ is so sánh kiểu của kết quả của một biểu thức có tương thích với một lớp hay không
BiểuThức  is   TênLớp
+ GetType trả về kiểu của một biến khi chạy (Runtime):
System.Type TênBiến.GetType()

Graduated

Viết phân trang
private void XTable_BindData(bool IsBind)
{
	xTable.DataSource = new MyDataSource(xTable, UserDealerISN, UserISN, SortBy, SortDiect); //ReformatWithTimeZone(ds, new string[] { "ntfDate" });
	if (IsBind)
		xTable.DataBind();
}

class MyDataSource : WebControlLibrary.XTable.DataSource.BasicDataSource
        {
            private int UserDealerISN, UserISN;
            string _sortBy, _sortDirect;
            protected Database Database
            {
                get
                {
                    return WebLibrary.Global.Database;
                }
            }

            public MyDataSource(WebControlLibrary.XTable.XTable table, int userDealerISN, int userISN, string sortBy, string sortDirect)
                : base(table)
            {
                this.UserDealerISN = userDealerISN;
                this.UserISN = userISN;
                this._sortBy = sortBy;
                this._sortDirect = sortDirect;
            }


            public override object GetPageData(int pageSize, int page, out object resultCount)
            {
                int i;
                string conditionProcessor = " and ProcessorISN = " + UserISN;
                string conditionQuery = "DealerISN = " + UserDealerISN + conditionProcessor;
                
                string strSortField = (_sortBy == string.Empty) ? "docProcessorStatus asc, docAddedDate asc" : "docProcessorStatus asc, docAddedDate asc, " +_sortBy;
                string strSortType = _sortDirect;
                if (_sortDirect == string.Empty && _sortBy != string.Empty)
                    strSortType = (this.XTable.SortDirection.ToString() == "Ascending") ? "asc" : "desc";

                if (this.XTable.SearchFilter != "")
                    conditionQuery += " and " + this.XTable.SearchFilter.Replace("docProcessorStatus='4'", "docProcessorStatus in (0, 1)");

                //if (this.XTable.SearchFilter.IndexOf("docProcessorStatus='4'") > -1)
                //    conditionQuery += " and " + this.XTable.SearchFilter.Replace("docProcessorStatus='4'", "docProcessorStatus in (0, 1)");


                HttpContext.Current.Session["LeadNext_Condition"] = conditionQuery;
                HttpContext.Current.Session["LeadNext_SortBy"] = strSortField;
                HttpContext.Current.Session["LeadNext_SortDirect"] = strSortType;
                DataSet ds = Database.ExecuteStoredProcReturnDataSet("xp_debtext_document_mgmt_getpage",
                    new string[] { "condition", "nItemPage", "curpage", "NoRec", "sortBy", "sortDirect" },
                     new object[] { conditionQuery, pageSize, page + 1, DBNull.Value, strSortField, strSortType }, out i);

                resultCount = i;
                return ds;
            }
        }
Connect database Sql 		
public static Database Database
{
	get
	{
		if(Global.database==null)
		{
			Global.database = new Database(WebLibrary.Global.GetConfig("ReportConnectionString"));
			Global.database.Debug = true;
		}
		return Global.database;
	}
}

Send Mail
public static void SendHTMLMail(string serverMail, string from,string to,string cc,string subject,string content)
{			
	SmtpMail.SmtpServer = serverMail;								
	MailMessage aMail = new MailMessage();
	aMail.BodyFormat = MailFormat.Html;
	aMail.From = from;				
	aMail.To = to;
	if(cc != "")	
		aMail.Cc = cc;
	aMail.Subject = subject;
	aMail.Body = content;
	SmtpMail.Send(aMail);											
}
public static void sendMail(string from, string to, string subject, string body, bool isVietNam)
{

	MailMessage msg = new MailMessage();
	msg.To = to;
	msg.From = from;
	msg.Body = body;
	msg.Subject = subject;
	if(isVietNam)
		msg.BodyEncoding = Encoding.UTF8;
	else
		msg.BodyEncoding = Encoding.ASCII;
	msg.BodyFormat = MailFormat.Html;		
	
	SmtpMail.SmtpServer = GetConfig("SMTPServer");
	SmtpMail.Send(msg);
	
}
public static void sendMail(string from, string to, string subject, string body, bool isVietNam, string[] attachs)
{
	MailMessage msg = new MailMessage();
	if(attachs.Length==0)
		sendMail(from,to,subject,body,isVietNam);
	else
	{				
		for(int i=0;i<attachs.Length;i++)
		{
			if(attachs[i]!=null&&attachs[i]!="")
			{
				MailAttachment attachment = new MailAttachment(attachs[i]); //create the attachment
				msg.Attachments.Add(attachment);	//add the attachment
			}
		}
		msg.To = to;
		msg.From = from;
		msg.Body = body;
		msg.Subject = subject;
		if(isVietNam)
			msg.BodyEncoding = Encoding.UTF8;
		else
			msg.BodyEncoding = Encoding.ASCII;
		msg.BodyFormat = MailFormat.Html;

		SmtpMail.SmtpServer = GetConfig("SMTPServer");
		SmtpMail.Send(msg);
		
	}
}
Popup Log		
<table style="width: 100%; padding: 10px;">
    <tr>
       <td colspan="2">
            <%=HtmlContent%>
       </td>
    </tr>
</table>
public partial class DocumentStatusHistory : Common.BaseUserControl
{
	protected string HtmlContent = string.Empty;

	public int DocumentISN
	{
		get
		{
			return StringUtil.StringToInt(this.Request["DocumentISN"], -1);
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		Bind_Data();
	}
	private void Bind_Data()
	{
		DataSet docStatus = new DataSet();
		docStatus.ReadXml(Server.MapPath("~/XML/DocumentStatus.xml"));

		var dSet = new DataSet();
		if (DocumentISN > 0)
		{
			dSet = Database.ExecuteQueryWithParam("select * from Vw_DocumentStatusLog where DocumentISN=@DocumentISN order by updatedDate desc",
							new string[] { "DocumentISN" }, new object[] { DocumentISN });         
		}

		if (!Common.HtmlHelper.IsEmptyDataSet(dSet))
		{                
			foreach (DataRow row in dSet.Tables[0].Rows)
			{
				if (HtmlContent != "")
				{
					HtmlContent += "<br />====================================";
				}
				HtmlContent += string.Format("<br /><strong>{0} by {1}</strong>", Common.HtmlHelper.ConvertToDateString(row["updatedDate"], "MM/dd/yyyy, hh:mm tt"), row["updatedName"]);
				HtmlContent += string.Format("<br />Status change to: {0}", changeStatusFromDB(docStatus, ConvertObjectToInt(row["ProcessorStatus"])));                    
				
			}
		}
	}

	public string changeStatusFromDB(DataSet ds, int status)
	{
		if (!Common.HtmlHelper.IsEmptyDataSet(ds))
		{
			foreach (DataRow row in ds.Tables[0].Rows)
			{
				if (ConvertObjectToInt(row["value"], 0) == status)
				{
					return row["text"].ToString();
				}
			}
		}
		return string.Empty;
	}
}

ConfigurationManager.ConnectionStrings["ConnectionString"]

Get url windows 
window.location.href
Get url Popup
NamePopup.location.href
get param javascript
var url_string = window.location.href;        
var url = new URL(url_string);
var debtQty = url.searchParams.get("debtQty");
debtqty = url.searchparams.get("debtqty");
 
Call function from backend
Page.ClientScript.RegisterStartupScript(GetType(), "close", "LoadingDataClose();", true);

hight light
load formular
waiting submit
Can not export data
$('[id$='ctl00_progressPercent']).progressbar("value");
ctl00$txtSequenceFrom
ctl00_progressPercent
$('[id$='ctl00$txtSequenceFrom'])
(D/Total Pieces)
format number
    function formatBalance(element) {
    debugger;
        var result = $(element).val().replace(/[^0-9.]+/g, '');
        if(result.length > 12)
        {
            result = result.substr(0, 12);
        }
        $(element).val(numberWithCommas(result));
        calClientDebt();
    }

$(':input[type="submit"]').prop('disabled', false);

string filePath = "data.txt";
Console.WriteLine("Input");
string input = Console.ReadLine();

using (StreamWriter sw = new StreamWriter(filePath, true))
{
	sw.WriteLine(input);
}

Console.WriteLine("Do you want to read? Y/N");

string choice = Console.ReadLine();

if ("Y".Equals(choice))
{
	string data = string.Empty;
	using (StreamReader sr = new StreamReader(filePath))
	{
		while ((data = sr.ReadLine()) != null)
		{
			Console.WriteLine(data);
		}
	}
}

http://192.168.100.31/convert2pdf//api/ConvertHtml2Pdf?url=\\192.168.100.31\e\HE_Solar\Html\Proposal.html&fileName=wwwww
http://crm.roipredictions.com/(S(zrwmk1c2d4umtt2qhgbnri1h))/Proposal.aspx?isn=7
