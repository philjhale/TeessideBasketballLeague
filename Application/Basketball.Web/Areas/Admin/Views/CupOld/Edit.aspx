<%@ Page Title="Edit Cup" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" AutoEventWireup="true" 
    Inherits="System.Web.Mvc.ViewPage<Basketball.Core.Cup>" %>
<%@ Import Namespace="Basketball.Web.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Edit
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <% Html.RenderPartial("CupNav", ViewData); %>
    
    <h2>Edit Cup</h2>

    <% Html.RenderPartial("CupForm", ViewData.Model, ViewData); %>
</asp:Content>
