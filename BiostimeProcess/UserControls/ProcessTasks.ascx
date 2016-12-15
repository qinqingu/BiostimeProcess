<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProcessTasks.ascx.cs" Inherits="BiostimeProcess.UserControls.ProcessTasks" %>
<asp:Repeater ID="Tasks" runat="server">
    <HeaderTemplate>
        <table class="information" width="100%">
        <thead>
            <tr class="column">
                <th scope="col" style="width: 110px">任务状态</th>
                <th scope="col" style="width: 110px">步骤名</th>
                <th scope="col" style="width: 110px">接收人</th>
                <th scope="col" style="width: 110px">完成时间</th>
                <th scope="col" style="width: 110px">审批意见</th>
            </tr>
        </thead>
        <tbody>
    </HeaderTemplate>
    <AlternatingItemTemplate>
        <tr class="bg_color_f6">
            <td><%#Eval("Status") %></td>
            <td><%#Eval("StepName") %></td>
            <td><%#Eval("Assigner") %></td>
            <td><%#Eval("EndTime") %></td>
            <td><%#Eval("Desc") %></td>
        </tr>
    </AlternatingItemTemplate>
    <ItemTemplate>
        <tr>
            <td><%#Eval("Status") %></td>
            <td><%#Eval("StepName") %></td>
            <td><%#Eval("Assigner") %></td>
            <td><%#Eval("EndTime") %></td>
            <td><%#Eval("Desc") %></td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
    </tbody>
    </table>
    </FooterTemplate>
</asp:Repeater>