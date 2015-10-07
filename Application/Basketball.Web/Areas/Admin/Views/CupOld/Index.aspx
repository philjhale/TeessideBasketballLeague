<%@ Page Title="Cup List" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" AutoEventWireup="true" 
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<Basketball.Core.Cup>>" %>
<%@ Import Namespace="Basketball.Core" %>
<%@ Import Namespace="Basketball.Web.Controllers" %>
<%@ Import Namespace="Basketball.Core.Resources" %>

<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server">
Cups
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <% Html.RenderPartial("CupNav", ViewData); %>
    
    <h2>Cup List</h2>

    <table cellpadding="0" cellspacing="0" class="adminTable">
        <tr>
            <th>Cup Name</th>
            <th class="noBorder" colspan="2">
            </th>
        </tr>
		<%
		int i = 0;    
		foreach (Cup cup in ViewData.Model) { %>
			<tr class="<%= (i % 2 == 0) ? "oddRow" : "" %>">
				<td><%= cup.CupName%></td>
				<td class="noBorder buttonCol">
				    <%=Html.ActionLink<CupController>(c => c.Edit(cup.Id), "Edit")%>
                </td>
			</tr>
		<% i++;
        } %>
    </table>
</asp:Content>
