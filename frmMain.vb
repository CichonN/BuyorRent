'*********************************************
' Neina Cichon
' April 13, 2020
' Buy or Rent Program
'*********************************************

Public Class frmMain
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click

        'Closes Program
        Close()

    End Sub

    Private Sub btnBuy_Click(sender As Object, e As EventArgs) Handles btnBuy.Click

        'Change backcolor back to white in case of error
        txtHomeValue.BackColor = Color.White

        'Validate Home Value and only proceed if true
        If ValidateHomeValue() = True Then

            ' create new instance of frmBuy
            Dim Buy As New frmBuy

            'Show the form modally
            Buy.ShowDialog()

        End If
    End Sub

    Private Sub btnRent_Click(sender As Object, e As EventArgs) Handles btnRent.Click

        txtHomeValue.BackColor = Color.White

        If ValidateHomeValue() = True Then

            ' create new instance of frmRent
            Dim Rent As New frmRent

            'Show the form modally
            Rent.ShowDialog()
        End If

    End Sub
End Class
