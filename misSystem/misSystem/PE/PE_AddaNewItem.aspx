<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="PE_AddaNewItem.aspx.cs" Inherits="misSystem.PE.PE_AddaNewItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
  <script src="//code.jquery.com/jquery-1.10.2.js"></script>
  <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>  
    <script>
       $(function () {
           $("#<%=tbddate.ClientID%>").datepicker({
               minDate: "1"
               ,dateFormat: 'yy-mm-dd'
           });         
    });
    </script>    
    <script type="text/javascript">
        function tbck() {
            var tbddate = $("#<%=tbddate.ClientID%>").val();
            var value = $("#<%=tb_title.ClientID%>").val();
            var regex = /^[A-Z]{1}[0-9]{9}$/;
            var check = false;
            var msg = "請： Please:";
            if (value == "")
            {
                msg += (" 輸入工作項目！ enter the title!");                
            }
            else if (tbddate == "")
            {
                msg += (" 選擇預計完成日期！ choose a due date!");                
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
         <h1>新增工作項目(New task)</h1> 
         <asp:Label ID="Label1" runat="server"  Text="工作項目(Title)："  ></asp:Label>
         <asp:TextBox ID="tb_title" runat="server"></asp:TextBox>
         <br />
            <br />
        
         <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
         <asp:Label ID="Label2" runat="server" Text="工作內容(Task Description)："></asp:Label>
           <asp:TextBox ID="tb_desp" runat="server" TextMode="MultiLine"></asp:TextBox>
         <br />
          <asp:Button ID="bt_adddescription" runat="server" Text="新增(Add)" OnClick="bt_adddescription_Click" />
          <asp:Button ID="bt_editdescription" runat="server" Text="編輯(Edit)" OnClick="Button1_Click" />
         <br />
         <asp:Label ID="Label8" runat="server" Text="點選下方已新增的訊息列，進行編輯或移除："></asp:Label>    
          <br />  
         <asp:Label ID="Label10" runat="server" Text=" Select a message below to edit or remove:"></asp:Label>            
         <br />
         <asp:ListBox ID="lb_taskdesp" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lb_taskdesp_SelectedIndexChanged" Height="75%" Width="75%"></asp:ListBox>          
          <asp:Button ID="bt_taskdespclr" runat="server" Text="移除(Remove)" OnClick="bt_taskdespclr_Click" />
         <br />
         <asp:Label ID="Label3" runat="server" Text="(最多新增五筆 Maximum: 5)"></asp:Label>            
          <br />
            <br />
         <asp:Label ID="Label4" runat="server" Text="工作產出(Project output)："></asp:Label>
         <asp:TextBox ID="tb_projoutput" runat="server" TextMode="MultiLine"></asp:TextBox>
        <br />
          <asp:Button ID="bt_addenditem" runat="server" Text="新增(Add)" OnClick="bt_addenditem_Click" />
         <asp:Button ID="bt_editenditem" runat="server" Text="編輯(Edit)" OnClick="bt_editenditem_Click"  />
         <br />
         <asp:Label ID="Label9" runat="server" Text="點選下方已新增的訊息列，進行編輯或移除："></asp:Label>    
          <br />  
         <asp:Label ID="Label11" runat="server" Text=" Select a message below to edit or remove:"></asp:Label>            
         <br />
          <asp:ListBox ID="lb_enditem" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lb_enditem_SelectedIndexChanged" Height="75%" Width="75%"></asp:ListBox>          
           <asp:Button ID="bt_enditemclr" runat="server" Text="移除(Remove)" OnClick="bt_enditemclr_Click" /> 
          <br />
         <asp:Label ID="Label5" runat="server" Text="(最多新增五筆 Maximum: 5)"></asp:Label>
         <br />        
         <br />
            <br />
          <asp:Label ID="Label6" runat="server" Text="預計完成日期(Due)："></asp:Label>
          <asp:TextBox ID="tbddate" runat="server"  onfocus="this.blur()" ></asp:TextBox>
         <asp:Label ID="Label7" runat="server" Text="(只能選擇今天以後的日期 Only the day after today is available)"></asp:Label>
     </ContentTemplate>
         </asp:UpdatePanel>
              <br />
            <br />
         <asp:Button ID="bt_submit" runat="server" Text="SUBMIT" OnClick="bt_submit_Click" OnClientClick="if(tbck()==false){return false;}else{if(confirm('確定繼續？\n Continue?')){}else{alert('已取消 Cancelled'); return false;}}" />
      <asp:Button ID="bt_list" runat="server" Text="List" OnClick="bt_list_Click" />
     </div>
</asp:Content>
