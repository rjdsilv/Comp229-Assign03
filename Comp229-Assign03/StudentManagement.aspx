<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentManagement.aspx.cs" Inherits="Comp229_Assign03.StudentManagement" %>
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

        <% if (string.IsNullOrEmpty(Request.QueryString["student"]))
            { %>
            <%-- New Student Panel --%>
            <div class="row">
                <asp:Panel ID="StudentsInclusionPanel" runat="server" CssClass="school-panel">
                    <div class="col-sm-12 school-panel-header"><span>New Student</span></div>
                    <div class="col-sm-5">
                        <asp:TextBox ID="StudentFirstMidNameTextBox" TextMode="SingleLine" runat="server" CssClass="school-input" placeholder="First and Middle Names"/>
                    </div>
                    <div class="col-sm-5">
                        <asp:TextBox ID="StudentLastNameTextBox" TextMode="SingleLine" runat="server" CssClass="school-input" placeholder="Last Name" />
                    </div>
                    <div class="col-sm-2" style="margin-top:16px">
                        <asp:Button ID="StudentSaveButton" TextMode="SingleLine" runat="server" CssClass="school-submit" Text=" Save Student " OnClick="StudentSaveButton_Click" />
                    </div>
                    <div class="col-sm-5">
                        <asp:RequiredFieldValidator ID="StudentFirstMidNameTextBox_RequiredFieldValidator" runat="server" ControlToValidate="StudentFirstMidNameTextBox" ErrorMessage="First / Middle name is required." SetFocusOnError="True" Display="Dynamic" CssClass="school-error-message"/>
                    </div>
                    <div class="col-sm-5">
                        <asp:RequiredFieldValidator ID="StudentLastNameTextBox_RequiredFieldValidator" runat="server" ControlToValidate="StudentLastNameTextBox" ErrorMessage="Last name is required." SetFocusOnError="True" Display="Dynamic" CssClass="school-error-message"/>
                    </div>
                    <div class="col-sm-2">&nbsp</div>
                </asp:Panel>
            </div>
            <br />

            <%-- Student List --%>
            <div class="row table-header">
                <div class="col-sm-3"><span>Student Id</span></div>
                <div class="col-sm-4"><span>First and Middle Names</span></div>
                <div class="col-sm-3"><span>Last Name</span></div>
                <div class="col-sm-2"><span>Enrollment Date</span></div>
            </div>
            <asp:Repeater ID="StudentsRepeater" runat="server">
                <ItemTemplate>
                    <div class="row table-row-light">
                        <a href="StudentManagement.aspx?student=<%# Eval("Id") %>">
                            <div class="col-sm-3"><span><%# Eval("Id") %></span></div>
                            <div class="col-sm-4"><span><%# Eval("FirstMidName") %></span></div>
                            <div class="col-sm-3"><span><%# Eval("LastName") %></span></div>
                            <div class="col-sm-2"><span><%# Eval("EnrollmentDate") %></span></div>
                        </a>
                    </div>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <div class="row table-row-light-alternate">
                        <a href="StudentManagement.aspx?student=<%# Eval("Id") %>">
                            <div class="col-sm-3"><span><%# Eval("Id") %></span></div>
                            <div class="col-sm-4"><span><%# Eval("FirstMidName") %></span></div>
                            <div class="col-sm-3"><span><%# Eval("LastName") %></span></div>
                            <div class="col-sm-2"><span><%# Eval("EnrollmentDate") %></span></div>
                        </a>
                    </div>
                </AlternatingItemTemplate>
            </asp:Repeater>
        <% } else { %>
            <asp:Panel ID="StudentDetailsPanel" runat="server" CssClass="school-panel">
                <div class="col-sm-12 school-panel-header"><span>Student Details</span></div>
                <br />
                <div class="table-header-light">
                    <div class="col-sm-2"><span>Student Id</span></div>
                    <div class="col-sm-3"><span>First and Middle Names</span></div>
                    <div class="col-sm-3"><span>Last Name</span></div>
                    <div class="col-sm-2"><span>Enrollment Date</span></div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-1"></div>
                </div>
                <div class="table-row-light">
                    <div class="col-sm-2"><span><%= selectedStudent.Id %></span></div>
                    <div class="col-sm-3"><span><%= selectedStudent.FirstMidName %></span></div>
                    <div class="col-sm-3"><span><%= selectedStudent.LastName %></span></div>
                    <div class="col-sm-2"><span><%= selectedStudent.EnrollmentDate %></span></div>
                    <%-- Thanks to https://www.iconfinder.com --%>
                    <div class="col-sm-1"><a href="UpdateStudent.aspx?student=<%= selectedStudent.Id %>"><asp:Image AlternateText="Update Student" ImageUrl="~/Images/UpdateIcon.png" CssClass="action-button" ToolTip="Update Student" runat="server" /></a></div>
                    <%-- Thanks to https://vignette.wikia.nocookie.net --%>
                    <div class="col-sm-1"><asp:ImageButton ID="RemoveStudentImageButton" AlternateText="Remove Student" ImageUrl="~/Images/DeleteIcon.png" CssClass="action-button" ToolTip="Remove Student" OnClick="RemoveStudentImageButton_Click" runat="server" Style="margin-top: 12px;" /></div>
                </div>
            </asp:Panel>

            <%-- Courses enrolled --%>
            <% if (enrolledCourses > 0) { %>
                <hr />
                <asp:Panel ID="StudentEnrollmentsPanel" runat="server" CssClass="school-panel">
                    <div class="col-sm-12 school-panel-header"><span>Enrolled Courses</span></div>
                    <br />
                    <div class="table-header-light">
                        <div class="col-sm-2"><span>Course Id</span></div>
                        <div class="col-sm-4"><span>Title</span></div>
                        <div class="col-sm-1"><span>Credits</span></div>
                        <div class="col-sm-1"><span>Grade</span></div>
                        <div class="col-sm-3"><span>Department</span></div>
                        <div class="col-sm-1"></div>
                    </div>
                    <asp:Repeater ID="StudentEnrolledCoursesRepeater" runat="server">
                        <ItemTemplate>
                            <div class="table-row-light">
                                <a href="CourseManagement.aspx?course=<%# Eval("Course.Id") %>">
                                    <div class="col-sm-2"><span><%# Eval("Course.Id") %></span></div>
                                    <div class="col-sm-4"><span><%# Eval("Course.Title") %></span></div>
                                    <div class="col-sm-1"><span><%# Eval("Course.Credits") %></span></div>
                                    <div class="col-sm-1"><span><%# Eval("Grade") %></span></div>
                                    <div class="col-sm-3"><span><%# Eval("Course.Department.Name") %></span></div>
                                </a>
                                <div class="col-sm-1">
                                    <%-- Thanks to https://vignette.wikia.nocookie.net --%>
                                    <asp:ImageButton 
                                        ID="RemoveEnrollmentImageButton" 
                                        AlternateText="Unenroll Course" 
                                        ImageUrl="~/Images/DeleteIcon.png" 
                                        CssClass="action-button" 
                                        ToolTip="Unenroll Course" 
                                        OnClick="RemoveEnrollmentImageButton_Click" 
                                        CommandArgument='<%# Eval("Id") %>'
                                        runat="server" />
                                </div>
                            </div>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <div class="table-row-light-alternate">
                                <a href="CourseManagement.aspx?course=<%# Eval("Course.Id") %>">
                                    <div class="col-sm-2"><span><%# Eval("Course.Id") %></span></div>
                                    <div class="col-sm-4"><span><%# Eval("Course.Title") %></span></div>
                                    <div class="col-sm-1"><span><%# Eval("Course.Credits") %></span></div>
                                    <div class="col-sm-1"><span><%# Eval("Grade") %></span></div>
                                    <div class="col-sm-3"><span><%# Eval("Course.Department.Name") %></span></div>
                                </a>
                                <div class="col-sm-1">
                                    <%-- Thanks to https://vignette.wikia.nocookie.net --%>
                                    <asp:ImageButton 
                                        ID="RemoveEnrollmentImageButton" 
                                        AlternateText="Unenroll Course" 
                                        ImageUrl="~/Images/DeleteIcon.png" 
                                        CssClass="action-button" 
                                        ToolTip="Unenroll Course" 
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
                <asp:Panel ID="NoCoursesPanel" runat="server" CssClass="school-message">
                    <hr />
                    <div>The student <%= selectedStudent.LastName %>, <%= selectedStudent.FirstMidName %> is not enrolled in any course.</div>
                    <hr />
                </asp:Panel>
            <% } %>

            <%-- Courses not enrolled --%>
            <% if (notEnrolledCourses > 0) { %>
                <asp:Panel ID="StudentUnenrollmentsPanel" runat="server" CssClass="school-panel">
                    <div class="col-sm-12 school-panel-header"><span>Courses Not Enrolled</span></div>
                    <br />
                    <div class="table-header-light">
                        <div class="col-sm-2"><span>Course Id</span></div>
                        <div class="col-sm-4"><span>Title</span></div>
                        <div class="col-sm-2"><span>Credits</span></div>
                        <div class="col-sm-3"><span>Department</span></div>
                        <div class="col-sm-1"></div>
                    </div>
                    <asp:Repeater ID="StudentNotEnrolledCoursesRepeater" runat="server">
                        <ItemTemplate>
                            <div class="table-row-light">
                                <a href="CourseManagement.aspx?course=<%# Eval("Id") %>">
                                    <div class="col-sm-2"><span><%# Eval("Id") %></span></div>
                                    <div class="col-sm-4"><span><%# Eval("Title") %></span></div>
                                    <div class="col-sm-2"><span><%# Eval("Credits") %></span></div>
                                    <div class="col-sm-3"><span><%# Eval("Department.Name") %></span></div>
                                </a>
                                <div class="col-sm-1">
                                    <%-- Thanks to https://vignette.wikia.nocookie.net --%>
                                    <asp:ImageButton 
                                        ID="CourseEnrollmentImageButton" 
                                        AlternateText="Enroll Course" 
                                        ImageUrl="~/Images/InsertIcon.png" 
                                        CssClass="action-button" 
                                        Width="25px"
                                        ToolTip="Enroll Course" 
                                        OnClick="CourseEnrollmentImageButton_Click"
                                        CommandArgument='<%# Eval("Id") %>'
                                        runat="server" />
                                </div>
                            </div>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <div class="table-row-light-alternate">
                                <a href="CourseManagement.aspx?course=<%# Eval("Id") %>">
                                    <div class="col-sm-2"><span><%# Eval("Id") %></span></div>
                                    <div class="col-sm-4"><span><%# Eval("Title") %></span></div>
                                    <div class="col-sm-2"><span><%# Eval("Credits") %></span></div>
                                    <div class="col-sm-3"><span><%# Eval("Department.Name") %></span></div>
                                </a>
                                <div class="col-sm-1">
                                    <%-- Thanks to https://vignette.wikia.nocookie.net --%>
                                    <asp:ImageButton 
                                        ID="CourseEnrollmentImageButton" 
                                        AlternateText="Enroll Course" 
                                        ImageUrl="~/Images/InsertIcon.png" 
                                        CssClass="action-button"
                                        Width="25px"
                                        ToolTip="Enroll Course" 
                                        OnClick="CourseEnrollmentImageButton_Click" 
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
