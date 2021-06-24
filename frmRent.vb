'*********************************************
' Neina Cichon
' April 13, 2020
' Programming 1
' Buy or Rent Program
'*********************************************

Public Class frmRent
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click

        'Closes the Buy Form Window
        Close()

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click

        'Clears out textboxes
        txtFirstName.Clear()
        txtLastName.Clear()

        'reset Combboxes
        cboMonths.SelectedIndex = -1
        cboUtilities.SelectedIndex = -1
        cboParking.SelectedIndex = -1

        'Clear out ListBox
        lstResults.Items.Clear()

        'set focus back to top textbox
        txtFirstName.Focus()

    End Sub

    Private Sub frmRent_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Carry over Home Value Price from Main Form
        lblHomeValue.Text = dblHomeValue.ToString("c")

    End Sub

    Private Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles btnCalculate.Click

        ''Declare Constants
        'Const cdblParking As Double = 50
        'Const cdblUtilities As Double = 0.2

        'Declare Variables
        Dim dblRent As Double
        Dim intMonths As Integer
        Dim strFirstName As String
        Dim strLastName As String
        Dim dblTotalRent As Double
        Dim dblUtilities As Double
        Dim dblParking As Double


        'Set backcolors back to white in case of error
        txtFirstName.BackColor = Color.White
        txtLastName.BackColor = Color.White
        cboMonths.BackColor = Color.White
        cboUtilities.BackColor = Color.White
        cboParking.BackColor = Color.White



        If ValidateInput(strFirstName, strLastName, cboMonths, cboUtilities, cboParking) Then

            'Call Calculation Procedures
            intMonths = CalcMonths(cboMonths)
            dblRent = CalcRent(dblHomeValue)
            dblUtilities = CalcUtilities(cboUtilities)
            dblParking = CalcParking(cboParking)
            dblTotalRent = CalcTotalRent(dblRent, dblUtilities, dblParking)

            'Call Display
            Display(dblRent, cboUtilities.Text, cboParking.Text, dblTotalRent)

            'Display proper case for names in textboxes
            txtFirstName.Text = StrConv(txtFirstName.Text, vbProperCase)
            txtLastName.Text = StrConv(txtLastName.Text, vbProperCase)


        End If

    End Sub

    Function ValidateInput(ByRef FirstName As String, ByRef LastName As String, ByRef Months As ComboBox, ByRef Utilities As ComboBox, ByRef Parking As ComboBox) As Boolean

        'Validate all input 
        If ValidateFirstName(FirstName) = True Then
            If ValidateLastName(LastName) = True Then
                If ValidateMonths(Months) = True Then
                    If ValidateUtilities(Utilities) = True Then
                        If ValidateParking(Parking) = True Then
                            Return True
                        Else
                            Return False
                        End If
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function



    'Create Function to Validate First Name
    Function ValidateFirstName(ByRef FirstName As String) As Boolean

        'Validate First Name is required - Display error message if empty
        If txtFirstName.Text = String.Empty Then
            MessageBox.Show("Please enter first name.")
            txtFirstName.BackColor = Color.Yellow     'Set color to yellow
            txtFirstName.Focus()    'set focus back to errored cell for user
            Return False
        Else
            FirstName = txtFirstName.Text
            Return True
        End If

    End Function

    'Create Function to Validate Last Name
    Function ValidateLastName(ByRef LastName As String) As Boolean

        'Validate Last Name is required - Display error message if empty
        If txtLastName.Text = String.Empty Then
            MessageBox.Show("Please enter last name.")
            txtLastName.BackColor = Color.Yellow     'Set color to yellow
            txtLastName.Focus()    'set focus back to errored cell for user
            Return False
        Else
            LastName = txtLastName.Text
            Return True
        End If

    End Function

    'Create Function to Validate Months Combobox
    Function ValidateMonths(ByRef Month As ComboBox) As Boolean

        'Checking to make sure state has been selected
        If cboMonths.SelectedIndex = -1 Then
            MessageBox.Show("Please select amount of months.")
            cboMonths.BackColor = Color.Yellow
            cboMonths.Focus()
            Return False
        Else
            Return True
        End If

    End Function

    'Create Function to Validate Utilities Combobox
    Function ValidateUtilities(ByRef Utilities As ComboBox) As Boolean

        'Checking to make sure state has been selected
        If cboUtilities.SelectedIndex = -1 Then
            MessageBox.Show("Please select Utilities option.")
            cboUtilities.BackColor = Color.Yellow
            cboUtilities.Focus()
            Return False
        Else
            Return True
        End If

    End Function

    'Create Function to Validate Parking Combobox
    Function ValidateParking(ByRef Parking As ComboBox) As Boolean

        'Checking to make sure state has been selected
        If cboParking.SelectedIndex = -1 Then
            MessageBox.Show("Please select Covered Parking option.")
            cboParking.BackColor = Color.Yellow
            cboParking.Focus()
            Return False
        Else
            Return True
        End If

    End Function

    'Create procedure to Calculate months
    Function CalcMonths(ByVal Months As ComboBox) As Integer

        'Declare Variables
        Dim intMonths As Integer

        'Assign value to Months
        If cboMonths.Text.ToUpper = "6 MONTHS" Then
            intMonths = 6
        Else
            If cboMonths.Text.ToUpper = "12 MONTHS" Then
                intMonths = 12
            Else
                If cboMonths.Text.ToUpper = "18 MONTHS" Then
                    intMonths = 18
                Else
                    intMonths = 24
                End If
            End If
        End If

        'Return Result
        Return intMonths

    End Function

    Function CalcRent(ByVal HomeValue) As Double

        'Declare Variables
        Dim dblRent As Double

        'Calculate Rent
        dblRent = ((HomeValue * 0.1) / 12)

        'Return Result
        Return dblRent

    End Function

    'Calculate rent with Utilities
    Function CalcUtilities(ByVal Utilities As ComboBox) As Double

        'Declare Constants and Variables
        Const cdblUtilities As Double = 0.2
        Dim dblUtilities As Double

        'Assign value to Months
        If cboUtilities.Text.ToUpper = "NO" Then
            dblUtilities = 0
        Else
            dblUtilities = cdblUtilities
        End If

        'Return Result
        Return dblUtilities

    End Function

    'Calculate Rent with Parking 
    Function CalcParking(ByVal Parking As ComboBox) As Double

        'Declare Constants and Variables
        Const cdblParking As Double = 50
        Dim dblParking As Double

        'Assign values to variable
        If cboParking.Text.ToUpper = "YES" Then
            dblParking = cdblParking
        Else
            dblParking = 0
        End If

        'Return Result
        Return dblParking

    End Function

    'Calculate total Rent
    Function CalcTotalRent(ByVal Rent As Double, ByVal Utilities As Double, ByVal Parking As Double) As Double

        'Declare Variables
        Dim dblTotalRent As Double

        'Calculate Total Rent
        dblTotalRent = (Rent + (Rent * Utilities)) + Parking

        'Return Result
        Return dblTotalRent

    End Function

    'Create Display Procedure
    Private Sub Display(ByRef Rent As Double, ByRef Utilities As String, ByRef Parking As String, ByRef TotalRent As Double)

        ' display the amounts formatted with headers and separaters
        lstResults.Items.Add("Monthly Rent" & vbTab & "Utilities" & vbTab & "Parking" & vbTab & "Total Monthly Rent")
        lstResults.Items.Add("---------------------------------------------------------------------------------------------")
        lstResults.Items.Add(Rent.ToString("C") & vbTab & vbTab & Utilities.ToString & vbTab & Parking.ToString & vbTab & TotalRent.ToString("C"))
        lstResults.Items.Add("")

    End Sub

    Private Sub ClearToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearToolStripMenuItem.Click

        ' call the clear button subroutine
        btnClear_Click(sender, e)

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click

        'Call Exit Button subroutine
        btnExit_Click(sender, e)

    End Sub

    Private Sub CalculateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CalculateToolStripMenuItem.Click

        'Call Calculate Button subroutine
        btnCalculate_Click(sender, e)

    End Sub
End Class