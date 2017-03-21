<%@ Page Title="" Language="C#" MasterPageFile="~/Service/ServiceMaster.master" AutoEventWireup="true" CodeBehind="Status.aspx.cs" Inherits="misSystem.Service.Service.test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="contentForService" runat="server">

    <div class="Lp550_Tm5_gradient40px">待處理</div>


    <div class="Lp550-Tm325-georgia18px">


        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">


            <asp:View ID="DefaultView" runat="Server">
                <table width="95%" id="table1" cellspacing="1" cellpadding="1">
                    <tr>
                        <td width="33%" bgcolor="#CCFFFF" style="border: 3px solid #000080">
                            <b>第一頁</b>
                        </td>
                        <td width="33%" bgcolor="#A6FFA6">
                            <asp:LinkButton ID="Default_NewsLink"
                                Text="第二頁"
                                CommandName="SwitchViewByID"
                                CommandArgument="NewsView"
                                Width="400"
                                Style="text-decoration: none"
                                runat="Server">
                            </asp:LinkButton>
                        </td>
                        <td width="33%" bgcolor="#FFD2D2">
                            <asp:LinkButton ID="Default_ShoppingLink"
                                Text="第三頁"
                                CommandName="SwitchViewByID"
                                CommandArgument="ShoppingView"
                                Width="400"
                                Style="text-decoration: none"
                                runat="Server">
                            </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" bgcolor="#CCFFFF" style="border: 3px solid #000080">
                            <br />

                            <asp:GridView ID="grid_UserList" runat="server" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="Both">
                                <AlternatingRowStyle BackColor="PaleGoldenrod" />
                                <Columns>                 
                                    <asp:TemplateField HeaderText="Shipping" ItemStyle-Height="35px" HeaderStyle-Height="35px">
                                        <ItemTemplate>
                                            <a href="/Service/ServiceForm/ShippingCheck.aspx?id=<%# Eval("shipping") %>"><%# Eval("shipping") %></a>
                                        </ItemTemplate>
                                        <ItemStyle Width="180px"></ItemStyle>
                                    </asp:TemplateField>
                                 
                                    <asp:TemplateField HeaderText="QC" ItemStyle-Width="180px">
                                        <ItemTemplate>
                                            <a href="/Service/ServiceForm/QC.aspx?id=<%# Eval("qc") %>"><%# Eval("qc") %></a>
                                        </ItemTemplate>
                                        <ItemStyle Width="180px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Investigate - service">
                                        <ItemTemplate>
                                            <a href="/Service/ServiceForm/InvestigationS.aspx?id=<%# Eval("igrs") %>"><%# Eval("igrs") %></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Investigate - PE" ItemStyle-Width="180px">
                                        <ItemTemplate>
                                            <a href="/Service/ServiceForm/InvestigationPE.aspx?id=<%# Eval("igrpe") %>"><%# Eval("igrpe") %></a>
                                        </ItemTemplate>
                                        <ItemStyle Width="180px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Meeting" ItemStyle-Width="180px">
                                        <ItemTemplate>
                                            <a href="/Service/Check/Check_PE.aspx?id=<%# Eval("pe") %>"><%# Eval("pe") %></a>
                                        </ItemTemplate>
                                        <ItemStyle Width="180px"></ItemStyle>
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle BackColor="Tan" />
                                <HeaderStyle BackColor="Tan" Font-Bold="True" />
                                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                <SortedAscendingCellStyle BackColor="#FAFAE7" />
                                <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                                <SortedDescendingCellStyle BackColor="#E1DB9C" />
                                <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                            </asp:GridView>
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:View>



            <asp:View ID="NewsView" runat="Server">
                <table width="95%" id="table2" cellspacing="1" cellpadding="1">
                    <tr>
                        <td width="33%" bgcolor="#CCFFFF">
                            <asp:LinkButton ID="News_DefaultLink"
                                Text="第一頁"
                                CommandName="SwitchViewByID"
                                CommandArgument="DefaultView"
                                Width="400"
                                Style="text-decoration: none"
                                runat="Server">
                            </asp:LinkButton>
                        </td>
                        <td width="33%" bgcolor="#A6FFA6" style="border: 3px solid #008000;">
                            <b>第二頁</b>
                        </td>
                        <td width="33%" bgcolor="#FFD2D2">
                            <asp:LinkButton ID="News_ShoppingLink"
                                Text="第三頁"
                                CommandName="SwitchViewByID"
                                CommandArgument="ShoppingView"
                                Width="400"
                                Style="text-decoration: none"
                                runat="Server">
                            </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" bgcolor="#A6FFA6" style="border: 3px solid #008000;">
                            <br />

                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="Both">
                                <AlternatingRowStyle BackColor="PaleGoldenrod" />
                                <Columns>

                                    <asp:TemplateField HeaderText="ServiceReport" ItemStyle-Height="35px" HeaderStyle-Height="35px">
                                        <ItemTemplate>

                                            <a href="/Service/ServiceForm/ServiceReport.aspx?id=<%# Eval("servicereport") %>"><%# Eval("servicereport") %></a>
                                        </ItemTemplate>
                                        <ItemStyle Width="180px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ServiceReport Confirm" ItemStyle-Width="180px">
                                        <ItemTemplate>
                                            <a href="/Service/Check/Check_ServiceReport.aspx?id=<%# Eval("checksr") %>"><%# Eval("checksr") %></a>
                                        </ItemTemplate>
                                        <ItemStyle Width="180px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ServiceFillForm">
                                        <ItemTemplate>
                                            <a href="/Service/ServiceForm/ServiceFillForm.aspx?id=<%# Eval("sff") %>"><%# Eval("sff") %></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DisposionProcess" ItemStyle-Width="180px">
                                        <ItemTemplate>
                                            <a href="/Service/ServiceForm/DisposionProcess.aspx?id=<%# Eval("dp") %>"><%# Eval("dp") %></a>
                                        </ItemTemplate>
                                        <ItemStyle Width="180px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SupervisorCheck" ItemStyle-Width="180px">
                                        <ItemTemplate>
                                            <a href="/Service/Check/Check_supervisor.aspx?id=<%# Eval("checksup") %>"><%# Eval("checksup") %></a>
                                        </ItemTemplate>
                                        <ItemStyle Width="180px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="審查" ItemStyle-Width="180px">
                                        <ItemTemplate>
                                            <a href="/Service/Check/Check_check.aspx?id=<%# Eval("check") %>"><%# Eval("check") %></a>
                                        </ItemTemplate>
                                        <ItemStyle Width="180px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SRC結案審查" ItemStyle-Width="180px">
                                        <ItemTemplate>
                                            <a href="/Service/Check/Check_nocapasrc.aspx?id=<%# Eval("nocapa") %>"><%# Eval("nocapa") %></a>
                                        </ItemTemplate>
                                        <ItemStyle Width="180px"></ItemStyle>
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle BackColor="Tan" />
                                <HeaderStyle BackColor="Tan" Font-Bold="True" />
                                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                <SortedAscendingCellStyle BackColor="#FAFAE7" />
                                <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                                <SortedDescendingCellStyle BackColor="#E1DB9C" />
                                <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                            </asp:GridView>


                            <br />
                        </td>
                    </tr>
                </table>
            </asp:View>


            <asp:View ID="ShoppingView" runat="Server">
                <table width="95%" id="table3" cellspacing="1" cellpadding="1">
                    <tr>
                        <td width="33%" bgcolor="#CCFFFF">
                            <asp:LinkButton ID="Shopping_DefaultLink"
                                Text="第一頁"
                                CommandName="SwitchViewByID"
                                CommandArgument="DefaultView"
                                Width="400"
                                Style="text-decoration: none"
                                runat="Server">
                            </asp:LinkButton>
                        </td>
                        <td width="33%" bgcolor="#A6FFA6">
                            <asp:LinkButton ID="Shopping_NewsLink"
                                Text="第二頁"
                                CommandName="SwitchViewByID"
                                CommandArgument="NewsView"
                                Width="400"
                                Style="text-decoration: none"
                                runat="Server">
                            </asp:LinkButton>
                        </td>
                        <td width="33%" bgcolor="#FFD2D2" style="border: 3px solid #800000;">
                            <b>第三頁</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" bgcolor="#FFD2D2" style="border: 3px solid #800000;">
                            <br />



                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="Both">
                                <AlternatingRowStyle BackColor="PaleGoldenrod" />
                                <Columns>

                                    <asp:TemplateField HeaderText="FillCAPA" ItemStyle-Height="35px" HeaderStyle-Height="35px">
                                        <ItemTemplate>

                                            <a href="/Service/ServiceForm/CAPA.aspx?id=<%# Eval("capa") %>"><%# Eval("capa") %></a>
                                        </ItemTemplate>
                                        <ItemStyle Width="180px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="主管審核" ItemStyle-Width="180px">
                                        <ItemTemplate>
                                            <a href="/Service/Check/Check_managercheck.aspx?id=<%# Eval("manager") %>"><%# Eval("manager") %></a>
                                        </ItemTemplate>
                                        <ItemStyle Width="180px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="矯正措施" ItemStyle-Width="180px">
                                        <ItemTemplate>
                                            <a href="/Service/Check/Check_redress.aspx?id=<%# Eval("redress") %>"><%# Eval("redress") %></a>
                                        </ItemTemplate>
                                        <ItemStyle Width="180px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="修正失效模式分析" ItemStyle-Width="180px">
                                        <ItemTemplate>
                                            <a href="/Service/ServiceForm/FMEA.aspx?id=<%# Eval("fmea") %>"><%# Eval("fmea") %></a>
                                        </ItemTemplate>
                                        <ItemStyle Width="180px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="驗證" ItemStyle-Width="180px">
                                        <ItemTemplate>
                                            <a href="/Service/ServiceForm/Validation.aspx?id=<%# Eval("validation") %>"><%# Eval("validation") %></a>
                                        </ItemTemplate>
                                        <ItemStyle Width="180px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="品保主管確認結案" ItemStyle-Width="180px">
                                        <ItemTemplate>
                                            <a href="/Service/Check/Check_QC.aspx?id=<%# Eval("qcmanager") %>"><%# Eval("qcmanager") %></a>
                                        </ItemTemplate>
                                        <ItemStyle Width="180px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SRC主席關閉CAPA" ItemStyle-Width="180px">
                                        <ItemTemplate>
                                            <a href="/Service/Check/Check_SRCCAPA.aspx?id=<%# Eval("srccapa") %>"><%# Eval("srccapa") %></a>
                                        </ItemTemplate>
                                        <ItemStyle Width="180px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SRC主席關閉CR" ItemStyle-Width="180px">
                                        <ItemTemplate>
                                            <a href="/Service/Check/Check_SRCCR.aspx?id=<%# Eval("srccr") %>"><%# Eval("srccr") %></a>
                                        </ItemTemplate>
                                        <ItemStyle Width="180px"></ItemStyle>
                                    </asp:TemplateField>
                                    

                                </Columns>
                                <FooterStyle BackColor="Tan" />
                                <HeaderStyle BackColor="Tan" Font-Bold="True" />
                                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                <SortedAscendingCellStyle BackColor="#FAFAE7" />
                                <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                                <SortedDescendingCellStyle BackColor="#E1DB9C" />
                                <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                            </asp:GridView>

                            <br />
                        </td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>

    </div>

</asp:Content>
