'*********************************************
' Neina Cichon
' April 13, 2020
' Programming 1
' Buy or Rent Program
'*********************************************


Public Class frmBuy
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click

        'Closes the Buy Form Window
        Close()

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click

        'Clear out TextBoxes
        txtFirstName.Clear()
        txtLastName.Clear()
        txtDownPayment.Clear()
        txtInterestRate.Clear()
        txtMortgageYears.Clear()

        'Clear out ListBox
        lstResults.Items.Clear()

        'Reset Focus to top textbox
        txtFirstName.Focus()

    End Sub

    Private Sub frmBuy_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Carry over Home Value Price from Main Form
        lblHomeValue.Text = dblHomeValue.ToString("c")

    End Sub

    Private Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles btnCalculate.Click

        'Declare Variables
        Dim strFirstName As String
        Dim strLastName As String
        Dim dblDownPayment As Double
        Dim dblInterestRate As Double
        Dim dblIntRate As Double
        Dim intMortgageYears As Integer
        Dim dblSalePrice As Double
        Dim dblPayment As Double
        Dim intNumberPayments As Integer

        'Change backcolors back to white in case of error
        txtFirstName.BackColor = Color.White
        txtLastName.BackColor = Color.White
        txtDownPayment.BackColor = Color.White
        txtInterestRate.BackColor = Color.White
        txtMortgageYears.BackColor = Color.White


        If ValidateInput(strFirstName, strLastName, dblDownPayment, dblInterestRate, intMortgageYears) = True Then

            'call calculation for DownPayment
            dblDownPayment = DownPaymentCalc(dblDownPayment)

            'Call Calculation for Mortgage Years
            intMortgageYears = MortgageYearsCalc()

            'Call Calculation for Number of Payments
            intNumberPayments = NumberPaymentsCalc(intMortgageYears)

            'Call Calculation for Sale Price
            dblSalePrice = SalePriceCalc(dblHomeValue, dblDownPayment)

            'call Calculation for Interest rate
            dblIntRate = InterestRateCalc(dblInterestRate)

            'Call Calculation for Mortgage
            dblPayment = MortgageCalc(dblSalePrice, dblInterestRate, intNumberPayments)

            'Call Display
            Display(dblSalePrice, dblInterestRate, dblPayment)

            'Display proper case for names in textboxes
            txtFirstName.Text = StrConv(txtFirstName.Text, vbProperCase)
            txtLastName.Text = StrConv(txtLastName.Text, vbProperCase)

            'Display Down Payment as Currency
            txtDownPayment.Text = FormatCurrency(txtDownPayment.Text)

        End If

    End Sub

    'Validate all input
    Function ValidateInput(ByRef FirstName, ByRef LastName, ByRef DownPayment, ByRef InterestRate, ByRef MortgageYears) As Boolean

        'Validate all inputs - only proceed if true
        If ValidateFirstName(FirstName) = True Then
            If ValidateLastName(LastName) = True Then
                If ValidateDownPayment(DownPayment) = True Then
                    If ValidateInterestRate(InterestRate) = True Then
                        If ValidateMortgageYears(MortgageYears) = True Then
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

        'Validate First Name- display error if empty
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

        'Validate First Name- display error if empty
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

    'Create procedure to Validate down Payment
    Function ValidateDownPayment(ByRef DownPayment As Double) As Boolean

        'Declare Variable
        Dim dblDownPayment As Double

        'Check downpayment to make sure it is Numeric
        If IsNumeric(txtDownPayment.Text) Then
            dblDownPayment = txtDownPayment.Text

            'Validate that downpayment is not negative
            If dblDownPayment >= 0 Then
                Return True
            Else
                'Display error if negative
                MessageBox.Show("Down payment can not be negative.")
                txtDownPayment.BackColor = Color.Yellow     'Set color to yellow
                txtDownPayment.Focus()    'set focus back to errored cell for user
                Return False
            End If
        Else
            'Display Error if Not Numeric
            MessageBox.Show("Please enter numbers only for Down Payment")
            txtDownPayment.BackColor = Color.Yellow     'Set color to yellow
            txtDownPayment.Focus()    'set focus back to errored cell for user
            Return False
        End If

    End Function

    'Create Procedure to validate Interest Rate
    Function ValidateInterestRate(ByRef InterestRate As Double) As Boolean

        'Validating that interest rate is numeric
        If IsNumeric(txtInterestRate.Text) Then
            InterestRate = txtInterestRate.Text
            'Validating that interest rate is between 4 and 8
            If InterestRate >= 4 And InterestRate <= 8 Then
                Return True
            Else
                'Display Error Message because number is not between 4 and 8, returning false
                MessageBox.Show("Mortgage Interest Rate must be a value between 4-8.")
                txtInterestRate.BackColor = Color.Yellow     'Set color to yellow
                txtInterestRate.Focus()    'set focus back to errored cell for user
                Return False
            End If
        Else
            'Display Error Message because interest rate is not numeric, returning false
            MessageBox.Show("Mortgage Interest Rate must be a value between 4-8.")
            txtInterestRate.BackColor = Color.Yellow     'Set color to yellow
            txtInterestRate.Focus()    'set focus back to errored cell for user
            Return False
        End If

    End Function

    Function ValidateMortgageYears(ByRef MortgageYears As Double) As Boolean

        'Declare variable
        Dim intMortgageYears As Integer

        'Validating that Number of years is numeric
        If IsNumeric(txtMortgageYears.Text) Then
            intMortgageYears = txtMortgageYears.Text
            If intMortgageYears >= 10 And intMortgageYears <= 40 Then
                Return True
            Else
                'Display Error Message because not between 10 and 40, returning false
                MessageBox.Show("Years of Mortgage must be a value between 10 and 40 years.")
                txtMortgageYears.BackColor = Color.Yellow     'Set color to yellow
                txtMortgageYears.Focus()    'set focus back to errored cell for user
                Return False
            End If
        Else
            'Display Error Message because interest rate is not numeric, returning false
            MessageBox.Show("Years of Mortgage must be a value between 10 and 40 years.")
            txtMortgageYears.BackColor = Color.Yellow     'Set color to yellow
            txtMortgageYears.Focus()    'set focus back to errored cell for user
            Return False
        End If

    End Function

    'Calculate Mortgage Years
    Function MortgageYearsCalc() As Integer

        'Declare Variable
        Dim intMortgageYears As Integer

        'assign value to variable
        intMortgageYears = txtMortgageYears.Text

        'Return Result
        Return intMortgageYears

    End Function

    'Calculate Down Payment
    Function DownPaymentCalc(ByRef DownPayment As Double) As Double

        'Declare Variable
        Dim dblDownPayment As Double

        'assign value to variable
        dblDownPayment = txtDownPayment.Text

        'Return Result
        Return dblDownPayment

    End Function

    'Calculate Number of payments based on years of mortgage
    Function NumberPaymentsCalc(ByRef MortgageYears As Integer) As Integer

        'Declare Variable
        Dim NumberPayments As Integer

        'multiply Years of Mortgage by 12 to get number of payments
        NumberPayments = (MortgageYears * 12)

        'Return Result
        Return NumberPayments

    End Function

    'Calculate Sale Price
    Function SalePriceCalc(ByRef HomeValue As Double, ByRef DownPayment As Double) As Double

        'Declare Variable
        Dim dblSalePrice As Double

        'Calculate Sale Price by subtracting DownPayment from Home Value
        dblSalePrice = (HomeValue - DownPayment)

        'Return Sale Price
        Return dblSalePrice

    End Function

    'Procedure to Calculate Interest
    Function InterestRateCalc(ByRef InterestRate As Double) As Double

        'Calculate Interest Rate by dividing by 100
        InterestRate = (InterestRate / 100) / 12

        'Return Interest Rate
        Return InterestRate

    End Function

    ' SalePrice is the total amount to be mortgaged, IE $105,000 with $5,000 down = #100,000, interest rate is the monthly interest rate, 
    ' Number payments Is the total monthly payments, Interest rate must me in decimal then divided by 12 for monthly rate IE 6%  
    ' Is 6/100 = .06, .06 / 12 = .005 which is the InterestRate needed, Number Payments is mortgage years * 12 months in a year. 
    ' 30 year mortgage = 30 * 12 or 360 Example $100,000 mortgage for 30 years at 6% payments will be $599.55
    Function MortgageCalc(ByVal SalePrice As Double, ByVal InterestRate As Double, ByVal NumberPayments As Integer) As Double

        Dim dblPayment As Double
        Dim dblDiscountFactor As Double

        dblDiscountFactor = (((1 + InterestRate) ^ NumberPayments) - 1) / (InterestRate * (1 + InterestRate) ^ NumberPayments)

        dblPayment = SalePrice / dblDiscountFactor

        Return dblPayment


    End Function


    'Create display procedure
    Private Sub Display(ByVal SalePrice As Double, ByVal InterestRate As Double, ByVal MonthlyPayment As Double)

        ' display the amounts formatted with headers and separaters
        lstResults.Items.Add("Sale Price" & vbTab & "Interest Rate" & vbTab & "Monthly Payment")
        lstResults.Items.Add("---------------------------------------------------------------------------------------")
        lstResults.Items.Add(SalePrice.ToString("C") & vbTab & FormatPercent(InterestRate) & vbTab & vbTab & MonthlyPayment.ToString("C"))
        lstResults.Items.Add("")

    End Sub

    Private Sub ClearToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearToolStripMenuItem.Click

        ' call the clear button subroutine for menu
        btnClear_Click(sender, e)

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click

        'Call Exit Button subroutine for menu
        btnExit_Click(sender, e)

    End Sub

    Private Sub CalculateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CalculateToolStripMenuItem.Click

        'Call Calculate Button subroutine for menu
        btnCalculate_Click(sender, e)

    End Sub

End Class