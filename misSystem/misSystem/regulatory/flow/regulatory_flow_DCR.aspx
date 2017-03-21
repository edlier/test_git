<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="regulatory_flow_DCR.aspx.cs" Inherits="misSystem.regulatory.flow.regulatory_flow_DCR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    
    <style>
        #dcr_middle {
            position: absolute;
            top: 170px;
            left: 300px;
        }

        #dcrFormTittle {
            font-weight: bold;
            font-size: 28px;
            color: blue;
        }

        #dcrFlowleft {
            position: absolute;
            margin-left: 50%;
            margin-top: 80px;
            left: -400px;
        }

        #dcrFlowRight {
            position: absolute;
            margin-left: 50%;
            margin-top: 80px;
            left: -80px;
        }
    </style>

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="dcrFormTittle">表單流程</div> 
        <div id="dcrFlowleft">
            <table>
                <tr>
                    <td>DCR送出 ↓</td>
                    <td></td>
                </tr>
                <tr>
                    <td>填寫會辦單 ↓</td>
                    <td>待填寫數量：<asp:Label ID="lab_dcr_waitingApprovalRecord" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td>單位主管核准 ↓</td>
                    <td>待核准數量：</td>
                </tr>
                <tr>
                    <td>PD&E主管確認 ↓</td>
                    <td>待確認數量：</td>
                </tr>
                <tr>
                    <td>PCMC主管確認 ↓</td>
                    <td>待確認數量：</td>
                </tr>
                <tr>
                    <td>審查</td>
                    <td>待審查數量：</td>
                </tr>
            </table>
        </div>
        <div id="dcrFlowRight">
            <table>
                <tr>
                    <td>會辦單 ↓</td>
                    <td></td>
                </tr>
                <tr>
                    <td>單位主管 ↓</td>
                    <td>待審核數量：</td>
                </tr>
                <tr>
                    <td>各單位會辦 ↓</td>
                    <td>待會辦數量：</td>
                </tr>
                <tr>
                    <td>主管審核 ↓</td>
                    <td>待審核數量：</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
