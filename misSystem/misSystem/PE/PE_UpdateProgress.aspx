<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="PE_UpdateProgress.aspx.cs" Inherits="misSystem.PE.PE_UpdateProgress" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link rel="stylesheet" href="/Scripts/jquery-ui.css" />    
    <script src="/Scripts/jquery-2.1.4.js"></script>
    <script src="/Scripts/jquery-ui.js"></script>  
     <script type="text/javascript">
        function tbck() {           
            var value = $("#<%=tb_progress.ClientID%>").val();
            var minprog =1 ;
            var regex = /^[0-9]+$/;
            var check = false;
            var msg = "請： Please:";
            if (value == "")
            {
                msg += (" 輸入數字 enter a number!"); 
            }           
            else
            {
                check = true;
            }
            if (check == false)
            {
                alert(msg);
            }
            return check;
        }      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="Lp400-Tm450-colorSet">
         <h1>UPDATE PROGRESS</h1>
         <asp:Label ID="Label1" runat="server" Text="工作項目(Title)："></asp:Label>
    <asp:Label ID="lbl_ShowTitle" runat="server" Text="Label"></asp:Label>
    <br/>
    <br/>
         <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
    <asp:Label ID="Label3" runat="server" Text="工作內容(Task Description)："></asp:Label><br />
    <asp:ListBox ID="lb_descriptions" runat="server" OnSelectedIndexChanged="lb_descriptions_SelectedIndexChanged" AutoPostBack="True" BackColor="#ABFFFF" EnableTheming="True" Rows="5" Height="75%" Width="75%"></asp:ListBox>
        <br />
          <asp:Label ID="lbl_descriptions" runat="server" Text="點選上方以顯示內容 Click to View"></asp:Label>
         <br/>
    <br/>
    <asp:Label ID="Label4" runat="server" Text="工作產出(ProjectOutput)："></asp:Label><br />
    <asp:ListBox ID="lb_enditems" runat="server" AutoPostBack="True" BackColor="#ABFFFF" OnSelectedIndexChanged="lb_enditems_SelectedIndexChanged" Rows="5" Height="75%" Width="75%"></asp:ListBox>
       <br />
           <asp:Label ID="lbl_enditems" runat="server" Text="點選上方以顯示內容 Click to View"></asp:Label>
         <br/>
    <br/>
         <asp:Label ID="Label2" runat="server" Text="進度(Progress)："></asp:Label>
         <asp:TextBox ID="tb_progress" runat="server" TextMode="Number" min="1" max="100"></asp:TextBox>
         <asp:Label ID="Label5" runat="server" Text="% Minimum:"></asp:Label>
         <asp:Label ID="lbl_minprog" runat="server" Text="Label"></asp:Label>
          <br/>
    <br/>
         <asp:Label ID="Label6" runat="server" Text="UPDATE："></asp:Label>
          <asp:TextBox ID="tb_message" runat="server" TextMode="MultiLine"></asp:TextBox>
         <asp:Button ID="bt_addmessage" runat="server" Text="新增(Add)" OnClick="bt_addmessage_Click" />
          
         <asp:Button ID="bt_editmessage" runat="server" Text="編輯(Edit)" OnClick="Button1_Click" />
         <br />
         <asp:Label ID="Label8" runat="server" Text="點選下方已新增的訊息列，進行編輯或移除："></asp:Label>    
          <br />  
         <asp:Label ID="Label9" runat="server" Text=" Select a message below to edit or remove:"></asp:Label>            
         <br />
          <asp:ListBox ID="lb_message" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lb_message_SelectedIndexChanged" Rows="5" Height="75%" Width="75%"></asp:ListBox>          
           <asp:Button ID="bt_messageclr" runat="server" Text="移除(Remove)" OnClick="bt_messageclr_Click" /> 
          <br />
         <asp:Label ID="Label7" runat="server" Text="(最多新增五筆 Maximum: 5)"></asp:Label>
         <br />
         <br />
                  </ContentTemplate>
         </asp:UpdatePanel>
         <asp:Button ID="bt_submit" runat="server" Text="SUBMIT" OnClick="bt_submit_Click" OnClientClick="if(tbck()==false){return false;}else{if(confirm('確定繼續？\n Continue?')){}else{alert('已取消 Cancelled'); return false;}}" />         
        <asp:Button ID="bt_back" runat="server" Text="Back" OnClick="bt_back_Click" />
     </div>
</asp:Content>
