<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admins/Admin.Master" CodeBehind="AlertNotificationList_Current.aspx.vb" Inherits="PDRM_SPE.AAlertNotificationList_Current" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!--Notification-->
    <script type="text/javascript">
        function fetchAlerts(severity, limit) {
            const param = {
                processstatus: 1,
                severity: severity,
                limit: limit
            };
            //get alert list
            $.ajax({
                type: "POST",
                url: "../GetData.aspx/GetAlertList",
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    // Parse the JSON response
                    var alerts = JSON.parse(response.d);

                    // Clear the existing content
                    $('#dv' + severity + 'list').empty();
                    let count = 0;
                    if (alerts.length > 0) {
                        count = alerts.length;
                        alerts.forEach(alert => {
                            var tableHTML = "<table class='table table-striped table-bordered' id='" + alert.fldid + "'>"
                            tableHTML += "<tr><td><i class='fa fa-user'></i></td><td style='text-align: left'>" + alert.fldoppname + " - " + alert.fldoppicno + "</td></tr>"
                            tableHTML += "<tr><td><i class='fa fa-warning'></i></td><td style='text-align: left'>" + alert.fldmsg + "</td></tr>"
                            tableHTML += "<tr><td><i class='fa fa-clock-o'></i></td><td style='text-align: left'>" + alert.flddatetime.replace("T", " ") + "</td></tr>"
                            tableHTML += "<tr><td colspan='2' align='center'><button class='btn blue btn-xs' id='btnAcknowledge' onclick=\"OpenPopupWindow('../admins/AlertInfo.aspx?id=" + alert.fldid + "&i=" + alert.fldmd5 + "',1280,800);return false;\"><%=GetText("Acknowledge")%></button></td></tr>"
                            tableHTML += "</table>"
                            $('#dv' + severity + 'list').append(tableHTML);
                        });
                    }

                    //count unprocess alert
                    $.ajax({
                        type: "POST",
                        url: "../GetData.aspx/GetAlertListCount",
                        data: JSON.stringify({ processstatus: 1, severity: severity }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            $('#txt' + severity + 'count').text(" - " + count + " / " + response.d);
                        },
                        error: function (xhr, status, error) {
                            console.error("Failed to fetch data: " + error);
                        }
                    });
                },
                error: function (xhr, status, error) {
                    console.error("Failed to fetch data: " + error);
                }
            });
        }

        function callFetchAlerts() {
            fetchAlerts("high", 10);
            fetchAlerts("medium", 10);
            fetchAlerts("low", 10);
        }

        function initAlert() {
            callFetchAlerts();
            setInterval(callFetchAlerts, 10000); // 10 seconds
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .notification {
            min-height: 100vh;
            max-height: 100vh;
            overflow-y: scroll;
        }

            .notification table td {
                padding: 5px 5px !important;
            }
    </style>
    <!-- PAGE HEADER -->
    <h3 class="page-title">
        <asp:Label runat="server" ID="lblPageTitle" Text="User List"></asp:Label></h3>
    <!-- PAGE HEADER -->
    <div class="clearfix">
    </div>
    <!-- PAGE BODY -->
    <div class="row">
        <div class="col-md-12">
            <asp:UpdatePanel runat="server" ID="upTable">
                <ContentTemplate>
                    <!-- SEARCH -->
                    <div class="col-md-4">
                        <div class="portlet box red">
                            <div class="portlet-title">
                                <div class="caption">
                                    <label class="caption-subject bold uppercase">
                                        <%=GetText("High")%>
                                        <label id="txthighcount"></label>
                                    </label>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-body notification" id="dvhighlist">
                                        <!-- TABLE -->
                                        <!-- TABLE -->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="portlet box yellow-gold">
                            <div class="portlet-title">
                                <div class="caption">
                                    <label class="caption-subject bold uppercase">
                                        <%=GetText("Medium")%>
                                        <label id="txtmediumcount"></label>
                                    </label>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-body notification" id="dvmediumlist">
                                        <!-- TABLE -->
                                        <!-- TABLE -->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="portlet box yellow-crusta">
                            <div class="portlet-title">
                                <div class="caption">
                                    <label class="caption-subject bold uppercase">
                                        <%=GetText("Low")%>
                                        <label id="txtlowcount"></label>
                                    </label>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-body notification" id="dvlowlist">
                                        <!-- TABLE -->
                                        <!-- TABLE -->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- SEARCH -->
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <!-- PAGE BODY -->
    <asp:HiddenField runat="server" ID="hfConfirm" Value="Confirm update details?" ClientIDMode="Static" />

</asp:Content>

