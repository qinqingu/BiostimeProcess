<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="_PaymentLayout.Master" CodeBehind="Start.aspx.cs" Inherits="BiostimeProcess.Pages.Payment.Start" %>
<%@ MasterType VirtualPath="_PaymentLayout.master" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            var $startProcessButton = $('#' + '<%= StartProcessButton.ClientID %>');
            var $submitProcessButton = $('#' + '<%= SubmitProcessButton.ClientID %>');

            $startProcessButton.add($submitProcessButton).click(function () {
                return webui.faPaymentProcess.submitValidate();
            });
        });
    </script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <table class="width100" style="height: 30px;">
        <tr>
            <td style="width: 450px;"></td>
            <td style="text-align: center;">
                <asp:LinkButton ID="StartProcessButton" CssClass="button01" runat="server"  Visible="False" OnClick="StartProcessButton_Click">
                    <span class="l"></span>
                    <span class="m">发起</span>
                    <span class="r"></span>
                </asp:LinkButton>
                <asp:LinkButton ID="SubmitProcessButton" CssClass="button01" runat="server" onclick="SubmitProcessButton_Click"  Visible="False">
                    <span class="l"></span>
                    <span class="m">提交</span>
                    <span class="r"></span>
                </asp:LinkButton>
                <asp:LinkButton ID="AbortProcessButton" CssClass="button01" runat="server"  onclick="AbortProcessButton_Click"  Visible="False">
                    <span class="l"></span>
                    <span class="m">撤销</span>
                    <span class="r"></span>
                </asp:LinkButton>
            </td>
        </tr>
    </table>
</asp:Content>