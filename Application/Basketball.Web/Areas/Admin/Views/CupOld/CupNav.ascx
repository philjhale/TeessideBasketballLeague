<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Basketball.Web.Controllers" %>
<p>
    <%= Html.ActionLink<CupController>(c => c.Index(), "List Cups")%> |
    <%= Html.ActionLink<CupController>(c => c.Create(), "Create Cup")%>
</p>