<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Comp229_Assign03._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row table-header">
        <div class="col-sm-3"><span>Student Id</span></div>
        <div class="col-sm-3"><span>First and Middle Names</span></div>
        <div class="col-sm-3"><span>Last Name</span></div>
        <div class="col-sm-3"><span>Enrollment Date</span></div>
    </div>
    <asp:Repeater ID="studentsRepeater" runat="server">
        <ItemTemplate>
            <a href="Student.aspx?student=<%# Eval("Id") %>">
                <div class="row table-row">
                    <div class="col-sm-3"><span><%# Eval("Id") %></span></div>
                    <div class="col-sm-3"><span><%# Eval("FirstMidName") %></span></div>
                    <div class="col-sm-3"><span><%# Eval("LastName") %></span></div>
                    <div class="col-sm-3"><span><%# Eval("EnrollmentDate") %></span></div>
                </div>
            </a>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <a href="Student.aspx?student=<%# Eval("Id") %>">
                <div class="row table-row-alternate">
                    <div class="col-sm-3"><span><%# Eval("Id") %></span></div>
                    <div class="col-sm-3"><span><%# Eval("FirstMidName") %></span></div>
                    <div class="col-sm-3"><span><%# Eval("LastName") %></span></div>
                    <div class="col-sm-3"><span><%# Eval("EnrollmentDate") %></span></div>
                </div>
            </a>
        </AlternatingItemTemplate>
    </asp:Repeater>
</asp:Content>
