<%@ Page Title="Bar Ingresos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rptBarByCC.aspx.cs" Inherits="ONG.rptBarByCC" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script src="Scripts/Chart.min.js"></script>

<canvas id="myChart" width="200" height="200"></canvas>

</asp:Content>

