<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CourseManagement.aspx.cs" Inherits="Comp229_Assign03.CourseManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container body-content">
        <%-- Error Panel --%>
        <asp:Panel ID="ErrorPanel" runat="server" CssClass="school-error-message-hidden">
            <div><%= message %></div>
        </asp:Panel>

        <%-- Student success message panel --%>
        <asp:Panel ID="SuccessRemovalPanel" runat="server" CssClass="school-success-message-hidden">
            <div><%= message %></div>
        </asp:Panel>

        <% if (string.IsNullOrEmpty(Request.QueryString["course"]))
            { %>
            <%-- New Student Panel --%>
            <div class="row">
                <asp:Panel ID="CoursesInclusionPanel" runat="server" CssClass="school-panel">
                    <div class="col-sm-12 school-panel-header"><span>New Course</span></div>
                    <div class="col-sm-4">
                        <asp:TextBox ID="CourseTitleTextBox" TextMode="SingleLine" runat="server" CssClass="school-input" placeholder="Course Title"/>
                    </div>
                    <div class="col-sm-2">
                        <asp:TextBox ID="CourseCreditTextBox" TextMode="SingleLine" runat="server" CssClass="school-input" placeholder="Course Credits" />
                    </div>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="CourseDepartmentDropDown" runat="server" CssClass="school-input" />
                    </div>
                    <div class="col-sm-2" style="margin-top:16px">
                        <asp:Button ID="CourseSaveButton" TextMode="SingleLine" runat="server" CssClass="school-submit" Text=" Save Course " OnClick="CourseSaveButton_Click" />
                    </div>
                    <div class="col-sm-4">
                        <asp:RequiredFieldValidator ID="CourseTitleTextBox_RequiredFieldValidator" runat="server" ControlToValidate="CourseTitleTextBox" ErrorMessage="Course title is required." SetFocusOnError="True" Display="Dynamic" CssClass="school-error-message"/>
                    </div>
                    <div class="col-sm-2">
                        <asp:RequiredFieldValidator ID="CourseCreditTextBox_RequiredFieldValidator" runat="server" ControlToValidate="CourseCreditTextBox" ErrorMessage="Course credit is required." SetFocusOnError="True" Display="Dynamic" CssClass="school-error-message"/>
                    </div>
                    <div class="col-sm-6">&nbsp</div>
                </asp:Panel>
            </div>
            <br />

            <%-- Course List --%>
            <div class="row table-header">
                <div class="col-sm-3"><span>Course Id</span></div>
                <div class="col-sm-3"><span>Title</span></div>
                <div class="col-sm-3"><span>Credits</span></div>
                <div class="col-sm-3"><span>Department</span></div>
            </div>
            <asp:Repeater ID="CoursesRepeater" runat="server">
                <ItemTemplate>
                    <div class="row table-row-light">
                        <a href="CourseManagement.aspx?course=<%# Eval("Id") %>">
                            <div class="col-sm-3"><span><%# Eval("Id") %></span></div>
                            <div class="col-sm-3"><span><%# Eval("Title") %></span></div>
                            <div class="col-sm-3"><span><%# Eval("Credits") %></span></div>
                            <div class="col-sm-3"><span><%# Eval("Department.Name") %></span></div>
                        </a>
                    </div>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <div class="row table-row-light-alternate">
                        <a href="CourseManagement.aspx?course=<%# Eval("Id") %>">
                            <div class="col-sm-3"><span><%# Eval("Id") %></span></div>
                            <div class="col-sm-3"><span><%# Eval("Title") %></span></div>
                            <div class="col-sm-3"><span><%# Eval("Credits") %></span></div>
                            <div class="col-sm-3"><span><%# Eval("Department.Name") %></span></div>
                        </a>
                    </div>
                </AlternatingItemTemplate>
            </asp:Repeater>
        <% } else { %>
            <%-- Course details --%>
            <asp:Panel ID="CourseDetailsPanel" runat="server" CssClass="school-panel">
                <div class="col-sm-12 school-panel-header"><span>Course Details</span></div>
                <br />
                <div class="table-header-light">
                    <div class="col-sm-3"><span>Course Id</span></div>
                    <div class="col-sm-3"><span>Title</span></div>
                    <div class="col-sm-3"><span>Credits</span></div>
                    <div class="col-sm-3"><span>Department</span></div>
                </div>
                <div class="table-row-light">
                    <div class="col-sm-3"><span><%= selectedCourse.Id %></span></div>
                    <div class="col-sm-3"><span><%= selectedCourse.Title %></span></div>
                    <div class="col-sm-3"><span><%= selectedCourse.Credits %></span></div>
                    <div class="col-sm-3"><span><%= selectedCourse.Department.Name %></span></div>
                </div>
            </asp:Panel>

            <%-- Students enrolled --%>
            <% if (enrollmentCount > 0) { %>
                <hr />
                <asp:Panel ID="StudentsEnrolledPanel" runat="server" CssClass="school-panel">
                    <div class="col-sm-12 school-panel-header"><span>Students Enrolled</span></div>
                    <br />
                    <div class="table-header-light">
                        <div class="col-sm-2"><span>Student Id</span></div>
                        <div class="col-sm-4"><span>First and Middle Names</span></div>
                        <div class="col-sm-3"><span>Last Name</span></div>
                        <div class="col-sm-2"><span>Enrollment Date</span></div>
                        <div class="col-sm-1"></div>
                    </div>
                    <asp:Repeater ID="StudentsEnrolledRepeater" runat="server">
                        <ItemTemplate>
                            <div class="table-row-light">
                                <a href="StudentManagement.aspx?student=<%# Eval("Student.Id") %>">
                                    <div class="col-sm-2"><span><%# Eval("Student.Id") %></span></div>
                                    <div class="col-sm-4"><span><%# Eval("Student.FirstMidName") %></span></div>
                                    <div class="col-sm-3"><span><%# Eval("Student.LastName") %></span></div>
                                    <div class="col-sm-2"><span><%# Eval("Student.EnrollmentDate") %></span></div>
                                </a>
                                <div class="col-sm-1">
                                    <%-- Thanks to https://vignette.wikia.nocookie.net --%>
                                    <asp:ImageButton 
                                        ID="RemoveEnrollmentImageButton" 
                                        AlternateText="Unenroll Student" 
                                        ImageUrl="~/Images/DeleteIcon.png" 
                                        CssClass="action-button" 
                                        ToolTip="Unenroll Student" 
                                        OnClick="RemoveEnrollmentImageButton_Click" 
                                        CommandArgument='<%# Eval("Id") %>'
                                        runat="server" />
                                </div>
                            </div>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <div class="table-row-light-alternate">
                                <a href="StudentManagement.aspx?student=<%# Eval("Student.Id") %>">
                                    <div class="col-sm-2"><span><%# Eval("Student.Id") %></span></div>
                                    <div class="col-sm-4"><span><%# Eval("Student.FirstMidName") %></span></div>
                                    <div class="col-sm-3"><span><%# Eval("Student.LastName") %></span></div>
                                    <div class="col-sm-2"><span><%# Eval("Student.EnrollmentDate") %></span></div>
                                </a>
                                <div class="col-sm-1">
                                    <%-- Thanks to https://vignette.wikia.nocookie.net --%>
                                    <asp:ImageButton 
                                        ID="RemoveEnrollmentImageButton" 
                                        AlternateText="Unenroll Student" 
                                        ImageUrl="~/Images/DeleteIcon.png" 
                                        CssClass="action-button" 
                                        ToolTip="Unenroll Student" 
                                        OnClick="RemoveEnrollmentImageButton_Click" 
                                        CommandArgument='<%# Eval("Id") %>'
                                        runat="server" />
                                </div>
                            </div>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </asp:Panel>
                <hr />
            <% } else { %>
                <asp:Panel ID="NoEnrollmentsPanel" runat="server" CssClass="school-message">
                    <hr />
                    <div>The course <%= selectedCourse.Title %> has no students enrolled.</div>
                    <hr />
                </asp:Panel>
            <% } %>

            <%-- Students not enrolled --%>
            <% if (unenrollmentCount > 0) { %>
                <asp:Panel ID="StudentsUnenrolledPanel" runat="server" CssClass="school-panel">
                    <div class="col-sm-12 school-panel-header"><span>Students Not Enrolled</span></div>
                    <br />
                    <div class="table-header-light">
                        <div class="col-sm-2"><span>Student Id</span></div>
                        <div class="col-sm-4"><span>First and Middle Names</span></div>
                        <div class="col-sm-3"><span>Last Name</span></div>
                        <div class="col-sm-2"><span>Enrollment Date</span></div>
                        <div class="col-sm-1"></div>
                    </div>
                    <asp:Repeater ID="StudentsUnenrolledRepeater" runat="server">
                        <ItemTemplate>
                            <div class="table-row-light">
                                <a href="StudentManagement.aspx?student=<%# Eval("Id") %>">
                                    <div class="col-sm-2"><span><%# Eval("Id") %></span></div>
                                    <div class="col-sm-4"><span><%# Eval("FirstMidName") %></span></div>
                                    <div class="col-sm-3"><span><%# Eval("LastName") %></span></div>
                                    <div class="col-sm-2"><span><%# Eval("EnrollmentDate") %></span></div>
                                </a>
                                <div class="col-sm-1">
                                    <%-- Thanks to https://play.google.com --%>
                                    <asp:ImageButton 
                                        ID="EnrollStudentImageButton" 
                                        AlternateText="Enroll Student" 
                                        ImageUrl="~/Images/InsertIcon.png" 
                                        CssClass="action-button" 
                                        Width="25px"
                                        ToolTip="Enroll Student" 
                                        OnClick="EnrollStudentImageButton_Click" 
                                        CommandArgument='<%# Eval("Id") %>'
                                        runat="server" />
                                </div>
                            </div>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <div class="table-row-light-alternate">
                                <a href="StudentManagement.aspx?student=<%# Eval("Id") %>">
                                    <div class="col-sm-2"><span><%# Eval("Id") %></span></div>
                                    <div class="col-sm-4"><span><%# Eval("FirstMidName") %></span></div>
                                    <div class="col-sm-3"><span><%# Eval("LastName") %></span></div>
                                    <div class="col-sm-2"><span><%# Eval("EnrollmentDate") %></span></div>
                                </a>
                                <div class="col-sm-1">
                                    <%-- Thanks to https://play.google.com --%>
                                    <asp:ImageButton 
                                        ID="EnrollStudentImageButton" 
                                        AlternateText="Enroll Student" 
                                        ImageUrl="~/Images/InsertIcon.png" 
                                        CssClass="action-button" 
                                        Width="25px"
                                        ToolTip="Enroll Student" 
                                        OnClick="EnrollStudentImageButton_Click" 
                                        CommandArgument='<%# Eval("Id") %>'
                                        runat="server" />
                                </div>
                            </div>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </asp:Panel>
            <% } %>
        <% } %>
    </div>
</asp:Content>
