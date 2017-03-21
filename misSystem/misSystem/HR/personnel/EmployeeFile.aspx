<%@ Page Title="報到文件繳交" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="EmployeeFile.aspx.cs" Inherits="misSystem.HR.personnel.EmployeeFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../myCss/HR_css.css?v1.011.01" rel="stylesheet" />

    <style type="text/css">
        .auto-style1 {
            width: 25px;
        }

        .auto-style3 {
            width: 150px;
        }

        h2 {
            color: blue;
        }
    </style>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div id="out_div" style="top: 150px; width:1000px">
        <h2>報到文件繳交</h2>


        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <table style="width: 70%">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="工號：" /><asp:Label ID="lbl_empNo" runat="server" Text="Label" />&nbsp;&nbsp;&nbsp;&nbsp
                            <asp:Label ID="Label3" runat="server" Text="姓名：" /><asp:Label ID="lbl_empName" runat="server" Text="Label" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label5" runat="server" Text="到職日：" /><asp:Label ID="lbl_inD" runat="server" Text="Label" /><asp:HiddenField ID="hf_company" runat="server" />
                            <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
                        </td>
                        <td style=" ">
                            <asp:ImageButton ID="imgbtnEdit" ToolTip="Edit(編輯)" runat="server" ImageUrl="~/img/images_edit.png" Width="30" Height="30" Visible="false" />
                            <asp:ImageButton ID="imgbtnSave" ToolTip="Save(儲存單筆)" runat="server" ImageUrl="~/img/images_save01.png" Width="30" Height="30" OnClick="imgbtnSave_Click" />
                            
                            <asp:ImageButton ID="imgbtnUpdate" ToolTip="Update(更新)" runat="server" ImageUrl="~/img/images_update02.jpg" Width="30" Height="30" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:ImageButton ID="imgbtnExit" ToolTip="Cancal(取消)" runat="server" ImageUrl="~/img/images_exit02.jpg" Width="30" Height="30" OnClick="imgbtnExit_Click" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReturn" runat="server" Text="上一頁" OnClick="btnReturn_Click" />
                            &nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>

                <%----%>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="cb_all" runat="server" Text="全選" Checked="false" Visible="false" OnCheckedChanged="cb_all_CheckedChanged" AutoPostBack="true" />
                <asp:GridView ID="grid_new" runat="server" Style="overflow: auto" GridLines="None" BorderLine="None" AutoGenerateColumns="False" Width="100%" OnRowEditing="grid_new_RowEditing" OnRowUpdating="grid_new_RowUpdating" OnRowCancelingEdit="grid_new_RowCancelingEdit"  >
				        <Columns>                            
					        <asp:TemplateField HeaderText="No.">
						        <ItemTemplate>
							        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" Hight="30px" />
						        </ItemTemplate>
						        <ItemStyle Width="40px" HorizontalAlign="Center" BorderStyle="None" />
					        </asp:TemplateField>
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Code" Visible="false" >
                                <ItemTemplate>
                                    <asp:Label ID="lbl_code" runat="server" Text='<%# Eval("Code") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="繳交狀態" Visible="false" >
                                <ItemTemplate>
                                    <asp:CheckBox ID="cb_YN1" runat="server" />
                                    <%--<asp:Label ID="lbl_FileYN" runat="server" Text='<%# Eval("FileYN") %>' />--%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="繳交狀態">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_YN" runat="server" Text='<%# Eval("checkYN")%>' />
                                </ItemTemplate>
                              <%--  <FooterTemplate>                                    
                                   <asp:CheckBox ID="cb_YN" runat="server" />
                                </FooterTemplate>--%>
                                <EditItemTemplate>
                                   <asp:CheckBox ID="cb_edtYN" runat="server" Checked='<%# (Eval("FileYN").ToString().Equals("Y"))%>' />
                                    <%--<asp:TextBox ID="edtNum" runat="server" Width="30px" Text='<%# Bind("num") %>'></asp:TextBox> Checked='<%# Eval("FileYN") %>'--%>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="4.5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="說明" Visible="false">
						        <ItemTemplate>
							        <asp:Label ID="lbl_codeN" Text='<%# Eval("CodeName") %>' runat="server" Hight="30px" />
						        </ItemTemplate>
						        <ItemStyle HorizontalAlign="Left" BorderStyle="None" Width="45%" />
					        </asp:TemplateField>	
                            <asp:TemplateField HeaderText="說明文字">
						        <ItemTemplate>
							        <asp:Label ID="lbl_descr" Text='<%# Eval("CodeDescr") %>' runat="server" Hight="30px" />
						        </ItemTemplate>
						        <ItemStyle HorizontalAlign="Left" BorderStyle="None" Width="45%" />
					        </asp:TemplateField>	
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="上傳檔案" Visible="false" >
                                <ItemTemplate>
                                    <asp:FileUpload ID="fup_file" runat="server" />    
                                </ItemTemplate>                               
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>		
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="上傳檔案" >
                                <ItemTemplate>
                                    <a href="#" onclick="<%# "window.open('"+ Eval("htmlStr") +"', null,'width=800,height=600,scrollbars=yes,left=350,top=200,right=5,bottom=5')" %>"><%# Eval("FileName") %></a>
                                    <asp:Label ID="lbl_FileNTxt" runat="server" Text='<%# Eval("FileName") %>' visible="false" />
                                </ItemTemplate>
                                <%--<FooterTemplate> 
                                    <asp:FileUpload ID="fup_fileFooter" runat="server" />                                  
                                </FooterTemplate>--%>
                                <EditItemTemplate>
                                    <asp:FileUpload ID="fup_editFile" runat="server"  />                                  
                                    <%--<asp:TextBox ID="edtNum" runat="server" Width="30px" Text='<%# Bind("num") %>'></asp:TextBox> Checked='<%# Eval("FileYN") %>'--%>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="5%" />
                            </asp:TemplateField>		
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="備註" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="tb_commentTxt" runat="server" Text='<%# Eval("Comment") %>'  Width="80%"/>                                    
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="20%"/>
                            </asp:TemplateField>	
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="備註">
                                <ItemTemplate>
                                    <asp:Label ID="tb_comment" runat="server" Text='<%# Eval("Comment") %>' />
                                </ItemTemplate>
                              <%--  <FooterTemplate>                                    
                                    <asp:TextBox ID="tb_comment" runat="server" Text='<%# Eval("Comment") %>' />
                                </FooterTemplate>--%>
                                <EditItemTemplate>
                                    <asp:TextBox ID="tb_editcomment" runat="server" Text='<%# Eval("Comment") %>' />
                                    <%--<asp:TextBox ID="edtNum" runat="server" Width="30px" Text='<%# Bind("num") %>'></asp:TextBox> Checked='<%# Eval("FileYN") %>'--%>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="15%"/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="修改User" Visible="false">
						        <ItemTemplate>
							        <asp:Label ID="lbl_updusr" Text='<%# Eval("UpdUserID") %>' runat="server" Hight="30px" />
						        </ItemTemplate>
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:TemplateField>
                            <asp:TemplateField HeaderText="修改User" >
						        <ItemTemplate>
							        <asp:Label ID="lbl_updusrN" Text='<%# Eval("UpdUserName") %>' runat="server" Hight="30px" />
						        </ItemTemplate>
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" Width="10%" />
					        </asp:TemplateField>                                                      				      
                            <asp:TemplateField HeaderText="修改時間">
						        <ItemTemplate>
							        <asp:Label ID="lbl_upddt" Text='<%# Eval("UpdDT","{0:yyyy/MM/dd HH:mm:ss}") %>' runat="server" Hight="30px" />
						        </ItemTemplate>
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" Width="5%" />
					        </asp:TemplateField>	                            
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="btn_Edit" runat="server" Text="修改" CommandName="Edit" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="btn_Update" runat="server" CommandName="Update" Text="更新" ></asp:Button>
                                    <asp:Button ID="btn_Cancel" runat="server" CommandName="Cancel" Text="取消" ></asp:Button>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="NewID" Visible="false" >
                                <ItemTemplate>
                                    <asp:Label ID="lbl_NID" runat="server" Text='<%# Eval("ID") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                            
				        </Columns>
				        <AlternatingRowStyle BackColor="PaleGoldenrod" Height="35" />
				        <HeaderStyle BackColor="YellowGreen" BorderStyle="None" Height="35" />
				        <PagerStyle CssClass="gridview" />
				        <FooterStyle BorderStyle="None" BackColor="White" BorderColor="White" />
				        <RowStyle Height="35" />
				        <SelectedRowStyle BackColor="LightCyan" forecolor="DarkBlue" font-bold="true" />
			        </asp:GridView>

                <asp:Button ID="btn_def" runat="server" Text="" CssClass="Georgia18px" OnClick="btn_def_Click" />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="imgbtnSave" />
                <asp:PostBackTrigger ControlID="grid_new" />
            </Triggers>
        </asp:UpdatePanel>
        <br />
        <br />
    </div>
</asp:Content>
