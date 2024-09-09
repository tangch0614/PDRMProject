Imports System.Net
Imports System.Net.Sockets
Imports System.Text

Partial Class Send
    Inherits System.Web.UI.Page

    Protected Sub btnSendMessage_Click(sender As Object, e As EventArgs)
        Try
            Dim client As New TcpClient("47.91.159.31", 8181)
            Dim stream As NetworkStream = client.GetStream()

            Dim message As String = "$GPRS,867255079747483;W043,1,1,1;!"
            Dim data As Byte() = Encoding.ASCII.GetBytes(message)

            ' Send the message to the server
            stream.Write(data, 0, data.Length)
            lblMessage.Text = "Sent: " & message

            'Receive the response from the server
            Dim buffer(1024) As Byte
            Dim bytesRead As Integer = stream.Read(buffer, 0, buffer.Length)
            Dim response As String = Encoding.ASCII.GetString(buffer, 0, bytesRead)
            lblMessage.Text &= "<br/>Received: " & response

            client.Close()
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Sub
End Class
