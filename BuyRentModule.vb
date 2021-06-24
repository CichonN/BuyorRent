'*********************************************
' Neina Cichon
' April 13, 2020
' Programming 1
' Buy or Rent Program
'*********************************************

Module BuyRentModule

    Public dblHomeValue As Double

    Function ValidateHomeValue() As Boolean

        ' Validate input and put it into variables
        If IsNumeric(frmMain.txtHomeValue.Text) Then
            dblHomeValue = frmMain.txtHomeValue.Text
            Return True
        Else
            frmMain.txtHomeValue.BackColor = Color.Yellow
            frmMain.txtHomeValue.Focus()
            MessageBox.Show("Please enter numbers only for Home Value.")
            Return False
        End If

    End Function

End Module
