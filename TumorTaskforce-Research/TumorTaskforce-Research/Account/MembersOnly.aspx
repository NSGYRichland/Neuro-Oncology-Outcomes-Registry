<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MembersOnly.aspx.cs" Inherits="Account_MembersOnly" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    .newStyle1 {
        font-family: Arial, Helvetica, sans-serif;
        font-size: 20px;
        font-weight: normal;
        font-style: normal;
        color: #006699;
        background-color: #E4E4E4;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p class="newStyle1">
    Welcome to the Members Only Page. You have sucessfully logged in!</p>
</asp:Content>

