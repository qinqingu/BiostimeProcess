<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="_FaProcessLayout.Master" CodeBehind="Approval.aspx.cs" Inherits="BiostimeProcess.Pages.Approval" %>

<%@ MasterType VirtualPath="_FaProcessLayout.master" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            var $submitProcessButton = $('#' + '<%= SubmitProcessButton.ClientID %>');
            var $returnPrevStepButton = $('#' + '<%= ReturnPrevStepButton.ClientID %>');
            var $returnStartorButton = $('#' + '<%= ReturnStartorButton.ClientID %>');
            $submitProcessButton.add($returnPrevStepButton).add($returnStartorButton).click(function () {
                return webui.faProcess.submitValidate();
            });
        });
    </script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <table>
        <tr class="border_b_1_da">
            <td style="width: 100px;">审批意见</td>
            <td>
                <textarea style="height: 100px; width: 912px;" maxlength="500" cols="45"
                          rows="5" id="descText" runat="server"></textarea>
            </td>
        </tr>
    </table>
    <table class="width100" style="height: 30px;">
        <tr>
            <td style="width: 450px;"></td>
            <td style="text-align: center;">
                <asp:LinkButton ID="SubmitProcessButton" CssClass="button01" runat="server" onclick="SubmitProcessButton_Click">
                    <span class="l"></span>
                    <span class="m">同意</span>
                    <span class="r"></span>
                </asp:LinkButton>
                <asp:LinkButton ID="ReturnPrevStepButton" CssClass="button01" runat="server" onclick="ReturnPrevStepButton_Click">
                    <span class="l"></span>
                    <span class="m">退回上一步</span>
                    <span class="r"></span>
                </asp:LinkButton>
                <asp:LinkButton ID="ReturnStartorButton" CssClass="button01" runat="server" onclick="ReturnStartorButton_Click">
                    <span class="l"></span>
                    <span class="m">退回发起人</span>
                    <span class="r"></span>
                </asp:LinkButton>
            </td>
        </tr>
    </table>
</asp:Content>
