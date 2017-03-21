<%@ Page Title="Daily Report" Language="C#" MasterPageFile="~/HR/mas_hr.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="HR_checkReport_day.aspx.cs" Inherits="misSystem.HR.HR_checkReport_day" %>

<%--<%@ Page Title="" Language="C#" MasterPageFile="~/HR/mas_hr.master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="misSystem.HR.WebForm1" %>--%>

<%@ Register Src="~/UserControl/uc_all_employee.ascx" TagPrefix="uc1" TagName="uc_all_employee" %>
<%@ Register Src="~/UserControl/uc_hr_memo_dl.ascx" TagPrefix="uc1" TagName="uc_hr_memo_dl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<script src="myJs/jquery-2.1.4.min.js"></script>
	<script src="myJs/jquery-ui.js"></script>
	<link href="myCss/jquery-ui.css" rel="stylesheet" />
	<link href="myCss/HR_css.css" rel="stylesheet" />
	<script src="myJs/HR_js.js"></script>
	 <script type="text/javascript">

		 var isSubmitted = false;

		 function preventMultipleSubmissions() {

			 if (!isSubmitted) {
				 $('#<%=get_check_btn.ClientID %>').val('Submitting.. Plz Wait..');

				isSubmitted = true;
				return true;

			}
			else {
				return false;
			}

		}
	</script>

	<style>
		#search_div {
			font-size: 14pt;
			margin: 0;
			padding: 0;
			border-bottom: 2pt groove #0a89e5;
		}

		#search_tb {
			margin: 0 auto;
			padding: 0;
			text-align: left;
			border-collapse: collapse;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentForHR" runat="server">
	<div class="HR_PublicTittle">日報表</div>
	<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

	<div id="out_div" style="top: 180px;">
		<%-- <asp:Panel ID="out_panel" runat="server">--%>

		<div id="search_div">
			<table id="search_tb">
				<tr>
					<td style="text-align: left;">User:<asp:Label ID="name_lab" runat="server" Text="---" CssClass="label-1"></asp:Label>
						<br />
						<asp:UpdatePanel ID="UpdatePanel1" runat="server">
							<ContentTemplate>
								Date:
								<asp:TextBox ID="date_text" runat="server" CssClass="datepicker" AutoPostBack="true" TextMode="DateTime" Style="margin-right: 1em;" OnTextChanged="date_text_TextChanged"></asp:TextBox>
								<%--Department: <asp:DropDownList runat="server" ID="dep_dl" Font-Size="14pt"></asp:DropDownList>--%>
							</ContentTemplate>
						</asp:UpdatePanel>
						<asp:Label ID="level_lab" runat="server" Text="---" CssClass="label-1" Visible="False"></asp:Label>
						<asp:Label ID="workNum_lab" runat="server" Text="---" CssClass="label-1" Visible="False"></asp:Label>
						<asp:Label ID="dep_lab" runat="server" Text="---" CssClass="label-1" Visible="False"></asp:Label>
					</td>
					<td>
						<asp:UpdatePanel ID="UpdatePanel3" runat="server">
							<ContentTemplate>
								<uc1:uc_all_employee runat="server" ID="uc_all_employee" />

							</ContentTemplate>
						</asp:UpdatePanel>
					</td>
					<td style="text-align: right; padding-left: 1em;">
						<uc1:uc_hr_memo_dl runat="server" ID="uc_hr_memo_dl" />
						<br />
						<asp:Button ID="search_btn" runat="server" Text="Search" OnClick="search_btn_Click" />
					</td>
					<td style="text-align: right; padding-left: 1em;">

						<asp:UpdatePanel ID="UpdatePanel4" runat="server">
							<ContentTemplate>
								<asp:Label ID="Label1" runat="server" Text="上次更新時間:"></asp:Label>
								<asp:Label ID="last_log_lab" runat="server" Text="---"></asp:Label>
								<br />
								<asp:Button ID="get_check_btn" runat="server" Text="Get CheckInOut" OnClientClick="return preventMultipleSubmissions();" OnClick="get_check_btn_Click" />
								<br />
								<asp:Label ID="get_infor_lab" runat="server" Text=""></asp:Label>
							</ContentTemplate>
						</asp:UpdatePanel>
					</td>
				</tr>
			</table>
		</div>

		<%--  </asp:Panel>--%>
		<br />

		<asp:Button ID="excel_btn" runat="server" Text="Download" OnClick="excel_btn_Click" Visible="false" />


		<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<asp:GridView ID="checkinout_grid" runat="server" Style="width: 100%; min-width: 100%; background-color: white; margin: 0px; text-align: center; font-size: 16pt;" CellPadding="3" ForeColor="#333333" GridLines="None" EmptyDataText="Searching didn't match any records." AllowPaging="false" OnPageIndexChanging="checkinout_grid_PageIndexChanging" AutoGenerateColumns="False" PageSize="2">
					<AlternatingRowStyle BackColor="White" />

					<Columns>
						<asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="WorkerNum">
							<ItemTemplate>
								<asp:Label ID="workerNum_lab" runat="server" Text='<%# Eval("WorkerNum") %>'></asp:Label>
							</ItemTemplate>
							<ItemStyle HorizontalAlign="Center" Wrap="True" />
						</asp:TemplateField>
						<asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Name">
							<ItemTemplate>
								<a href="<%# "HR_checkReport_personal.aspx?wn="+Eval("encode_WorkerNum")+para %>"><%# Eval("ChiName") %></a>
								<asp:Label ID="name_lab" runat="server" Text='<%# Eval("EnFName") %>' Visible="false"></asp:Label>
							</ItemTemplate>
							<ItemStyle HorizontalAlign="Center" Wrap="True" />
						</asp:TemplateField>
						<asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="CheckIn">
							<ItemTemplate>
								<asp:Label ID="inTime_lab" runat="server" Text='<%# Eval("CheckIn") %>'></asp:Label>
							</ItemTemplate>
							<ItemStyle HorizontalAlign="Center" Wrap="True" />
						</asp:TemplateField>
						<asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="CheckOut">
							<ItemTemplate>
								<asp:Label ID="outTime_lab" runat="server" Text='<%# Eval("CheckOut") %>'></asp:Label>
							</ItemTemplate>
							<ItemStyle HorizontalAlign="Center" Wrap="True" />
						</asp:TemplateField>
						<asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Status">
							<ItemTemplate>
								<a href="<%# "HR_checkReport_memo.aspx?wn="+Eval("encode_WorkerNum")+para %>"><%# Eval("memo") %></a>
								<asp:Label ID="outTime_lab" runat="server" Text='<%# Eval("memo") %>' Visible="false"></asp:Label>
							</ItemTemplate>
							<ItemStyle HorizontalAlign="Center" Wrap="True" />
						</asp:TemplateField>
						<asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="修正">
							<ItemTemplate>
								<a href="<%# "HR_IO_EditLog.aspx?id=1&wn="+Eval("encode_WorkerNum")+para2 %>"><%# Eval("log") %></a>
								<!--  <a href="HR_IO_EditLog.aspx"><%# Eval("log") %></a> -->
								<asp:Label ID="editLog_lab" runat="server" Text='修正' Visible="false"></asp:Label>
							</ItemTemplate>
							<ItemStyle HorizontalAlign="Center" Wrap="True" />
						</asp:TemplateField>
						<asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="請假">
							<ItemTemplate>
								<a target="_blank" href="<%# "HR_QuicklyImportDT.aspx?&wn="+Eval("encode_WorkerNum")+para2 %>">請假</a>
								<!--  <a href="HR_IO_EditLog.aspx"><%# Eval("log") %></a> -->
								<asp:Label ID="leave_lab" runat="server" Text='請假' Visible="false"></asp:Label>
							</ItemTemplate>
							<ItemStyle HorizontalAlign="Center" Wrap="True" />
						</asp:TemplateField>
						<asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="請假時間1">
							<ItemTemplate>
								<asp:Label ID="leaveT_lab" runat="server" Text='<%# Eval("LeaveT") %>'></asp:Label>
							</ItemTemplate>
							<ItemStyle HorizontalAlign="Center" Wrap="True" />
						</asp:TemplateField>
						<asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="請假時間2">
							<ItemTemplate>
								<asp:Label ID="leaveT2_lab" runat="server" Text='<%# Eval("LeaveT2") %>'></asp:Label>
							</ItemTemplate>
							<ItemStyle HorizontalAlign="Center" Wrap="True" />
						</asp:TemplateField>
					</Columns>
					<EditRowStyle BackColor="#2461BF" />
					<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
					<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
					<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
					<RowStyle BackColor="#EFF3FB" />
					<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
					<SortedAscendingCellStyle BackColor="#F5F7FB" />
					<SortedAscendingHeaderStyle BackColor="#6D95E1" />
					<SortedDescendingCellStyle BackColor="#E9EBEF" />
					<SortedDescendingHeaderStyle BackColor="#4870BE" />
				</asp:GridView>

			</ContentTemplate>
		</asp:UpdatePanel>
		<%-- </asp:Panel>--%>
	</div>

	<script>
		$(document).ready(function () {
			$('.datepicker').datepicker("option", "minDate", null);
			$('.datepicker').datepicker("option", "maxDate", null);

			//配合update panel不會重新執行ready事件
			//為update panel註冊事件，在update完成後執行initi_up();
			var prm = Sys.WebForms.PageRequestManager.getInstance();
			prm.add_pageLoaded(initi_up);
			function initi_up() {
				date_initi();
				$('.datepicker').datepicker("option", "minDate", null);
				$('.datepicker').datepicker("option", "maxDate", null);
			}
		});
	</script>
</asp:Content>
