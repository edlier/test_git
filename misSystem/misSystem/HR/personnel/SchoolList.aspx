<%@ Page Title="School List" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="SchoolList.aspx.cs" Inherits="misSystem.HR.personnel.SchoolList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="Lp550_Tm5_gradient40px">School List</div>
    <asp:Panel ID="Panel1" runat="server">
        <asp:DropDownList ID="drop_sort" runat="server" CssClass="Lp900-Tm390-georgia18px" AutoPostBack="false">
            <asp:ListItem Value="0">ID</asp:ListItem>
            <asp:ListItem Value="1">SchoolName</asp:ListItem>
            <asp:ListItem Value="2">City</asp:ListItem>
        </asp:DropDownList>
        <div class="Lp1080-Tm390">

            <asp:Button ID="btn_Sorting" runat="server" Text="DESC" CssClass="W100H28-georgia18px" OnClick="btn_Sorting_Click" />
        </div>

        <div class="Lp550-Tm325-georgia18px">
        <asp:GridView ID="grid_SchoolList" runat="server" Style="overflow: auto; width:900px;" GridLines="None" BorderLine="None" AutoGenerateColumns="False" AllowSorting="False" AllowPaging="False" PageSize="50">
            <Columns>                            
				<asp:TemplateField HeaderText="No.">
					<ItemTemplate>
						<asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" Hight="30px" />
					</ItemTemplate>
					<ItemStyle Width="40px" HorizontalAlign="Center" BorderStyle="None" />
				</asp:TemplateField>
				<asp:BoundField DataField="ID" HeaderText="ID(Hide)" Visible="false">
					<ItemStyle HorizontalAlign="Center" BorderStyle="None" />
				</asp:BoundField>
                <asp:BoundField DataField="SchoolName" HeaderText="SchoolName">
					<ItemStyle HorizontalAlign="Center" BorderStyle="None" />
				</asp:BoundField>
                <asp:BoundField DataField="City" HeaderText="City">
					<ItemStyle HorizontalAlign="Center" BorderStyle="None" />
				</asp:BoundField>
			</Columns>
		    <AlternatingRowStyle BackColor="PaleGoldenrod" Height="35" />
		    <HeaderStyle BackColor="YellowGreen" BorderStyle="None" Height="35" />
		    <PagerStyle CssClass="gridview" />
		    <FooterStyle BorderStyle="None" BackColor="White" BorderColor="White" />
		    <RowStyle Height="35" />
		    <SelectedRowStyle BackColor="LightCyan" forecolor="DarkBlue" font-bold="true" />
	    </asp:GridView>
    </div>

    </asp:Panel>
</asp:Content>
