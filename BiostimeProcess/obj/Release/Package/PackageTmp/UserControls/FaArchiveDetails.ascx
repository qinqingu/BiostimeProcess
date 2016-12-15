<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FaArchiveDetails.ascx.cs" Inherits="BiostimeProcess.UserControls.FaArchiveDetails" %>
<asp:Panel ID="pnlFaArchiveManagementContainer" style="display: none;" runat="server">
    <asp:Panel ID="pnlFaArchiveManagementErrorContainer" CssClass="error" runat="server">
    </asp:Panel>
    <table>
        <tr>
            <td style="width: 53px;">
                公司
            </td>
            <td style="width: 220px">
                <asp:TextBox ID="txtCompany" style="width: 200px;" CssClass="text_input" runat="server"></asp:TextBox>
            </td>
            <td style="width: 53px">
                年份
            </td>
            <td>
                <asp:TextBox ID="txtYear" style="width: 200px;" MaxLength="150" CssClass="text_input" runat="server">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                月份
            </td>
            <td>
                <asp:TextBox ID="txtMonth" style="width: 200px;" MaxLength="4" CssClass="text_input" runat="server">
                </asp:TextBox>
            </td>
            <td>
                凭证字
            </td>
            <td>
                <asp:TextBox ID="txtVoucherWord" MaxLength="50" style="width: 200px;" CssClass="text_input" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                凭证号
            </td>
            <td>
                <asp:TextBox ID="txtVoucherNumber" MaxLength="50" style="width: 200px;" CssClass="text_input" runat="server"></asp:TextBox>
            </td>
            <td>
                凭证券号
            </td>
            <td>
                <asp:TextBox ID="txtVoucherNo" MaxLength="50" style="width: 200px;" CssClass="text_input" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                存储位置
            </td>
            <td>
                <asp:TextBox ID="txtPath" MaxLength="50" style="width: 200px;" CssClass="text_input" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
            <td>
               存储柜号
            </td>
            <td>
                <asp:TextBox ID="txtCabinetNo" MaxLength="50" style="width: 200px;" CssClass="text_input" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td>
                借阅天数
            </td>
            <td>
                <asp:TextBox ID="txtDay" MaxLength="50" style="width: 200px;" CssClass="text_input" runat="server"></asp:TextBox>
            </td>
            <td>
              
            </td>
            <td>
                
            </td>
        </tr>
    </table>
    <div style="float: left; width: auto;">
        <asp:LinkButton ID="btnConfirm" CssClass="button02" runat="server">
            <span class="l"></span>
            <span class="m">确定</span>
            <span class="r"></span>
        </asp:LinkButton>
    </div>
    <div style="float: right; width: auto;">
        <asp:LinkButton ID="btnCancel" CssClass="button02" runat="server">
            <span class="l"></span>
            <span class="m">取消</span>
            <span class="r"></span>
        </asp:LinkButton>
    </div>
    <asp:HiddenField ID="archiveId" Value="" runat="server" />
    <asp:HiddenField runat="server" ID="stepValue"/>
    <asp:HiddenField ID="jieyueArchiveIds" Value="[]" runat="server" />
</asp:Panel>

<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        webui.faArchiveInfodetails.initialize({
            faArchiveManagementContainer: '<%= pnlFaArchiveManagementContainer.ClientID %>',
            faArchiveManagementErrorContainer: '<%= pnlFaArchiveManagementErrorContainer.ClientID %>',
            company: '<%= txtCompany.ClientID %>',
            year: '<%= txtYear.ClientID %>',
            month: '<%= txtMonth.ClientID %>',
            voucherWord: '<%= txtVoucherWord.ClientID %>',
            voucherNumber: '<%= txtVoucherNumber.ClientID %>',
            voucherNo: '<%= txtVoucherNo.ClientID %>',
            path: '<%= txtPath.ClientID %>',
            cabinetNo: '<%= txtCabinetNo.ClientID %>',
            day: '<%= txtDay.ClientID %>',
            confirmButton: '<%= btnConfirm.ClientID %>',
            cancelButton: '<%= btnCancel.ClientID %>',
            archiveId: '<%= archiveId.ClientID %>',
            stepValue: '<%= stepValue.ClientID %>',
            jieyueArchiveIds: '<%= jieyueArchiveIds.ClientID %>',
        });
    });
</script>