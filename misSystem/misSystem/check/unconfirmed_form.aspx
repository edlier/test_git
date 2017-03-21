<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="unconfirmed_form.aspx.cs" Inherits="misSystem.regulatory.unconfirmed_form"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CssFile/styles.css" rel="stylesheet" />
    <link href="../CssFile/default.css" rel="stylesheet" />
    <style>
        #ucf_title {
            font-weight: bold;
            font-size: 28px;
            color: blue;
        }

        #ucf_middle {
            position: absolute;
            top: 170px;
            left: 300px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="ucf_middle">
        <div id="ucf_title">待確認文件(Unconfirmed)</div>
        <br/><br/>
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="upp" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:MultiView ID="mulV" ActiveViewIndex="0" runat="server">
                    <asp:View ID="view1" runat="server">
                        <asp:Button ID="goto_v1"
                            Text="view1"
                            CommandName="SwitchViewByID"
                            CommandArgument="view1"
                            runat="server">
                        </asp:Button>
                        <asp:Button ID="goto_v2"
                            Text="view2"
                            CommandName="SwitchViewByID"
                            CommandArgument="view2"
                            runat="server">
                        </asp:Button>
                        <hr />
                        <asp:Table ID="table1" GridLines="Both" runat="server">
                            <asp:TableHeaderRow Width="100px" Height="30px" BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center">
                                <asp:TableHeaderCell> 2<%--RAAR MANAGER CHECK--%> </asp:TableHeaderCell>
                                <asp:TableHeaderCell> 3<%--RAAR REVIEW AND APPROVAL--%> </asp:TableHeaderCell>
                                <asp:TableHeaderCell> 4<%--RAAR CONCLUSION BY RESPONSIBLE DEP--%> </asp:TableHeaderCell>
                            </asp:TableHeaderRow>
                        </asp:Table>
                    </asp:View>

                    <asp:View ID="view2" runat="server">
                        <asp:Button ID="Button1"
                            Text="view1"
                            CommandName="SwitchViewByID"
                            CommandArgument="view1"
                            runat="server">
                        </asp:Button>
                        <asp:Button ID="Button2"
                            Text="view2"
                            CommandName="SwitchViewByID"
                            CommandArgument="view2"
                            runat="server">
                        </asp:Button>
                        <hr/>
                        <asp:Table ID="table2" GridLines="Both" runat="server">
                            <asp:TableHeaderRow Width="100px" Height="30px" BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center">
                                <asp:TableHeaderCell> 5 </asp:TableHeaderCell>
                                <asp:TableHeaderCell> 6<%--PD&E MANAGER EVALUATION ORREGULATORY OR SAFETY--%> </asp:TableHeaderCell>
                                <asp:TableHeaderCell> 7<%--PCMC INVENTORY--%> </asp:TableHeaderCell>
                                <asp:TableHeaderCell> 8<%--SRC CHECK--%> </asp:TableHeaderCell>
                            </asp:TableHeaderRow>
                        </asp:Table>
                    </asp:View>
                </asp:MultiView>
                <hr/>
            </ContentTemplate>
        </asp:UpdatePanel>










            <br/><br/>
            <%--<div>RAAR MANAGER CHECK</div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="GridView2_PageIndexChanging" CssClass="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="ID" ItemStyle-Width="80px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                        <ItemTemplate>
                            <div class="machine_padding">
                                <asp:Label ID="Label6" Text='<%# Eval("id") %>' runat ="server" />
                            </div>
                        </ItemTemplate>
                        <HeaderStyle Height="30px"></HeaderStyle>
                        <ItemStyle Height="30px" Width="80px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FillBy" ItemStyle-Width="100px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                        <ItemTemplate>
                            <div class="machine_padding">
                                <asp:Label ID="Label7" Text='<%# Eval("raarNo") %>' runat="server" />
                            </div>
                        </ItemTemplate>
                        <HeaderStyle Height="30px"></HeaderStyle>
                        <ItemStyle Height="30px" Width="100px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ResDept" ItemStyle-Width="100px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                        <ItemTemplate>
                            <div class="machine_padding">
                                <asp:Label ID="Label8" Text='<%# Eval("resdept") %>' runat="server" />
                            </div>
                        </ItemTemplate>
                        <HeaderStyle Height="30px"></HeaderStyle>
                        <ItemStyle Height="30px" Width="100px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FillDate" ItemStyle-Width="100px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                        <ItemTemplate>
                            <div class="machine_padding">
                                <asp:Label ID="Label9" Text='<%# Eval("fillDate") %>' runat="server" />
                            </div>
                        </ItemTemplate>
                        <HeaderStyle Height="30px"></HeaderStyle>
                        <ItemStyle Height="30px" Width="100px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="70px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                        <ItemTemplate>
                            <div class="machine_padding">
                                <a href="check_raarmg.aspx?id=<%# Eval("id") %>">Check</a>
                            </div>
                        </ItemTemplate>
                        <HeaderStyle Height="30px"></HeaderStyle>
                        <ItemStyle Height="30px" Width="70px"></ItemStyle>
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
            <div>RAAR REVIEW AND APPROVAL</div>
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="GridView2_PageIndexChanging" CssClass="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="ID" ItemStyle-Width="80px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                        <ItemTemplate>
                            <div class="machine_padding">
                                <asp:Label ID="Label6" Text='<%# Eval("id") %>' runat ="server" />
                            </div>
                        </ItemTemplate>
                        <HeaderStyle Height="30px"></HeaderStyle>
                        <ItemStyle Height="30px" Width="80px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FillBy" ItemStyle-Width="100px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                        <ItemTemplate>
                            <div class="machine_padding">
                                <asp:Label ID="Label7" Text='<%# Eval("fillBy") %>' runat="server" />
                            </div>
                        </ItemTemplate>
                        <HeaderStyle Height="30px"></HeaderStyle>
                        <ItemStyle Height="30px" Width="100px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ResDept" ItemStyle-Width="100px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                        <ItemTemplate>
                            <div class="machine_padding">
                                <asp:Label ID="Label8" Text='<%# Eval("resdept") %>' runat="server" />
                            </div>
                        </ItemTemplate>
                        <HeaderStyle Height="30px"></HeaderStyle>
                        <ItemStyle Height="30px" Width="100px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FillDate" ItemStyle-Width="100px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                        <ItemTemplate>
                            <div class="machine_padding">
                                <asp:Label ID="Label9" Text='<%# Eval("fillDate") %>' runat="server" />
                            </div>
                        </ItemTemplate>
                        <HeaderStyle Height="30px"></HeaderStyle>
                        <ItemStyle Height="30px" Width="100px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="70px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                        <ItemTemplate>
                            <div class="machine_padding">
                                <a href="check_raa.aspx?id=<%# Eval("id") %>">Check</a>
                            </div>
                        </ItemTemplate>
                        <HeaderStyle Height="30px"></HeaderStyle>
                        <ItemStyle Height="30px" Width="70px"></ItemStyle>
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
            <div>RAAR CONCLUSION BY RESPONSIBLE DEP</div>
                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="GridView2_PageIndexChanging" CssClass="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="ID" ItemStyle-Width="80px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                            <ItemTemplate>
                                <div class="machine_padding">
                                    <asp:Label ID="Label6" Text='<%# Eval("id") %>' runat ="server" />
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Height="30px"></HeaderStyle>
                            <ItemStyle Height="30px" Width="80px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FillBy" ItemStyle-Width="100px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                            <ItemTemplate>
                                <div class="machine_padding">
                                    <asp:Label ID="Label7" Text='<%# Eval("fillBy") %>' runat="server" />
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Height="30px"></HeaderStyle>
                            <ItemStyle Height="30px" Width="100px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ResDept" ItemStyle-Width="100px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                            <ItemTemplate>
                                <div class="machine_padding">
                                    <asp:Label ID="Label8" Text='<%# Eval("resdept") %>' runat="server" />
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Height="30px"></HeaderStyle>
                            <ItemStyle Height="30px" Width="100px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FillDate" ItemStyle-Width="100px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                            <ItemTemplate>
                                <div class="machine_padding">
                                    <asp:Label ID="Label9" Text='<%# Eval("fillDate") %>' runat="server" />
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Height="30px"></HeaderStyle>
                            <ItemStyle Height="30px" Width="100px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="70px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                            <ItemTemplate>
                                <div class="machine_padding">
                                    <a href="check_raafinal.aspx?id=<%# Eval("id") %>">Check</a>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Height="30px"></HeaderStyle>
                            <ItemStyle Height="30px" Width="70px"></ItemStyle>
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
                <br/><br/>
            <div>PD&E MANAGER EVALUATION ORREGULATORY OR SAFETY</div>
            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="ID" ItemStyle-Width="80px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <div class="machine_padding">
                            <asp:Label ID="Type_ID" Text='<%# Eval("id") %>' runat ="server" />
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Height="30px"></HeaderStyle>
                    <ItemStyle Height="30px" Width="80px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ChangePerposeBy" ItemStyle-Width="80px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <div class="machine_padding">
                            <asp:Label ID="Label1" Text='<%# Eval("changeby") %>' runat ="server" />
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Height="30px"></HeaderStyle>
                    <ItemStyle Height="30px" Width="80px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Priority" ItemStyle-Width="100px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <div class="machine_padding">
                            <asp:Label ID="Label2" Text='<%# Eval("priority") %>' runat="server" />
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Height="30px"></HeaderStyle>
                    <ItemStyle Height="30px" Width="100px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RnDprojectName" ItemStyle-Width="150px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <div class="machine_padding">
                            <asp:Label ID="Label3" Text='<%# Eval("RnDprojectName") %>' runat="server" />
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Height="30px"></HeaderStyle>
                    <ItemStyle Height="30px" Width="150px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DCRassignedNo" ItemStyle-Width="70px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <div class="machine_padding">
                            <asp:Label ID="Label4" Text='<%# Eval("DCRassignedNo") %>' runat="server" />
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Height="30px"></HeaderStyle>
                    <ItemStyle Height="30px" Width="70px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="changePerposeDate" ItemStyle-Width="70px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <div class="machine_padding">
                            <asp:Label ID="Label5" Text='<%# Eval("changePerposeDate2") %>' runat="server" />
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Height="30px"></HeaderStyle>
                    <ItemStyle Height="30px" Width="70px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="70px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <div class="machine_padding">
                            <a href="check_pdne.aspx?id=<%# Eval("id") %>">Check</a>
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Height="30px"></HeaderStyle>
                    <ItemStyle Height="30px" Width="70px"></ItemStyle>
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
        <br/><br/>
        <div>PCMC INVENTORY</div>
        <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="ID" ItemStyle-Width="80px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <div class="machine_padding">
                            <asp:Label ID="Type_ID" Text='<%# Eval("id") %>' runat ="server" />
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Height="30px"></HeaderStyle>
                    <ItemStyle Height="30px" Width="80px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ChangePerposeBy" ItemStyle-Width="80px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <div class="machine_padding">
                            <asp:Label ID="Label1" Text='<%# Eval("changeby") %>' runat ="server" />
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Height="30px"></HeaderStyle>
                    <ItemStyle Height="30px" Width="80px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Priority" ItemStyle-Width="100px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <div class="machine_padding">
                            <asp:Label ID="Label2" Text='<%# Eval("priority") %>' runat="server" />
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Height="30px"></HeaderStyle>
                    <ItemStyle Height="30px" Width="100px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RnDprojectName" ItemStyle-Width="150px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <div class="machine_padding">
                            <asp:Label ID="Label3" Text='<%# Eval("RnDprojectName") %>' runat="server" />
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Height="30px"></HeaderStyle>
                    <ItemStyle Height="30px" Width="150px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DCRassignedNo" ItemStyle-Width="70px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <div class="machine_padding">
                            <asp:Label ID="Label4" Text='<%# Eval("DCRassignedNo") %>' runat="server" />
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Height="30px"></HeaderStyle>
                    <ItemStyle Height="30px" Width="70px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="changePerposeDate" ItemStyle-Width="70px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <div class="machine_padding">
                            <asp:Label ID="Label5" Text='<%# Eval("changePerposeDate2") %>' runat="server" />
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Height="30px"></HeaderStyle>
                    <ItemStyle Height="30px" Width="70px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="70px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <div class="machine_padding">
                            <a href="check_pcmc.aspx?id=<%# Eval("id") %>">Check</a>
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Height="30px"></HeaderStyle>
                    <ItemStyle Height="30px" Width="70px"></ItemStyle>
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
        <br/><br/>
        <div>SRC CHECK</div>
        <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="ID" ItemStyle-Width="80px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <div class="machine_padding">
                            <asp:Label ID="Type_ID" Text='<%# Eval("id") %>' runat ="server" />
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Height="30px"></HeaderStyle>
                    <ItemStyle Height="30px" Width="80px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ChangePerposeBy" ItemStyle-Width="80px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <div class="machine_padding">
                            <asp:Label ID="Label1" Text='<%# Eval("changeby") %>' runat ="server" />
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Height="30px"></HeaderStyle>
                    <ItemStyle Height="30px" Width="80px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Priority" ItemStyle-Width="100px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <div class="machine_padding">
                            <asp:Label ID="Label2" Text='<%# Eval("priority") %>' runat="server" />
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Height="30px"></HeaderStyle>
                    <ItemStyle Height="30px" Width="100px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RnDprojectName" ItemStyle-Width="150px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <div class="machine_padding">
                            <asp:Label ID="Label3" Text='<%# Eval("RnDprojectName") %>' runat="server" />
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Height="30px"></HeaderStyle>
                    <ItemStyle Height="30px" Width="150px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DCRassignedNo" ItemStyle-Width="70px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <div class="machine_padding">
                            <asp:Label ID="Label4" Text='<%# Eval("DCRassignedNo") %>' runat="server" />
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Height="30px"></HeaderStyle>
                    <ItemStyle Height="30px" Width="70px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="changePerposeDate" ItemStyle-Width="70px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <div class="machine_padding">
                            <asp:Label ID="Label5" Text='<%# Eval("changePerposeDate2") %>' runat="server" />
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Height="30px"></HeaderStyle>
                    <ItemStyle Height="30px" Width="70px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="70px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <div class="machine_padding">
                            <a href="check_src.aspx?id=<%# Eval("id") %>">Check</a>
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Height="30px"></HeaderStyle>
                    <ItemStyle Height="30px" Width="70px"></ItemStyle>
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
        </asp:GridView>--%>
    </div>
</asp:Content>