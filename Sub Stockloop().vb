Sub Stockloop()
Dim ws As Worksheet
    For Each ws In ThisWorkbook.Worksheets
    ws.Activate
    
    'Set up

        'Set a variable for holding the ticker name, the column of interest
        Dim tickername As String
    
        'Set a varable for holding a total count on the total volume of trade
        Dim tickervolume As Double
        tickervolume = 0

        'Keep track of the location for each ticker name in the summary table
        Dim summary_ticker_row As Integer
        summary_ticker_row = 2
        
        'Note: Yearly Change is simply the difference: (Close Price at the end of a trading year - Open Price at the beginning of the trading year)
        'Percent change is a simple percent change -->((Close - Open)/Open)*100
        Dim open_price As Double
        'Set initial open_price. Other opening prices will be determined in the conditional loop.
        open_price = Cells(2, 3).Value
        
        Dim close_price As Double
        Dim yearly_change As Double
        Dim percent_change As Double

        'Label the Summary Table headers
        ws.Cells(1, 9).Value = "Ticker"
        ws.Cells(1, 10).Value = "Yearly Change"
        ws.Cells(1, 11).Value = "Percent Change"
        ws.Cells(1, 12).Value = "Total Stock Volume"
        
        'Lable the Cells
        ws.Cells(2, 15).Value = "Greatest % Increase"
        ws.Cells(3, 15).Value = "Greatest % Decrease"
        ws.Cells(4, 15).Value = "Greatest Total Volume"
        ws.Cells(1, 16).Value = "Ticker"
        ws.Cells(1, 17).Value = "Value"
        
        Dim GI As Double
        Dim GD As Double
        Dim GV As Double
        Dim IncN As String
        Dim DCN As String
        Dim Vol As String
        GI = ws.Cells(2, 11).Value
        GD = ws.Cells(2, 11).Value
        GV = ws.Cells(2, 12).Value
        IncN = ws.Cells(2, 9).Value
        DCN = ws.Cells(2, 9).Value
        Vol = ws.Cells(2, 9).Value
        For i = 2 To StockNumber
            If ws.Cells(i, 11).Value > GI Then
                GI = ws.Cells(i, 11).Value
                IncN = ws.Cells(i, 9).Value
             End If
             If ws.Cells(i, 11).Value < GD Then
               GD = ws.Cells(i, 11).Value
               DCN = ws.Cells(i, 9).Value
             End If
             If ws.Cells(i, 12).Value > GV Then
               GV = ws.Cells(i, 12).Value
               Vol = ws.Cells(i, 9).Value
             End If
             Next i
             ws.Range("P2").Value = IncN
             ws.Range("P3").Value = DCN
             ws.Range("P4").Value = Vol
             ws.Range("P5").Value = GI
             ws.Range("Q2").Value = GD
             ws.Range("Q3").Value = GV
             ws.Range("Q2:Q3").NumberFormat = "0.00%"
             
             
             
               
    
        
        
        
        
    
    

        'Count the number of rows in the first column.
        Lastrow = Cells(Rows.Count, 1).End(xlUp).Row

        'Loop through the rows by the ticker names
        'Make sure that the ticker names are sorted and are alpha-numeric/string variables.
        'Do a manual check.

        For i = 2 To Lastrow

            'Searches for when the value of the next cell is different than that of the current cell
            If Cells(i + 1, 1).Value <> Cells(i, 1).Value Then
        
              'Set the ticker name
              tickername = Cells(i, 1).Value

              'Add the volume of trade
              tickervolume = tickervolume + Cells(i, 7).Value

              'Print the ticker name in the summary table
              Range("I" & summary_ticker_row).Value = tickername

              'Print the trade volume for each ticker in the summary table
              Range("L" & summary_ticker_row).Value = tickervolume

              'Now collect information about closing price
              close_price = Cells(i, 6).Value

              'Calculate yearly change
              yearly_change = (close_price - open_price)
              
              'Print the yearly change for each ticker in the summary table
              Range("J" & summary_ticker_row).Value = yearly_change

             'Check for the non-divisibilty condition when calculating the percent change
                If (open_price = 0) Then

                    percent_change = 0

                Else
                    
                    percent_change = yearly_change / open_price
                
                End If

              'Print the yearly change for each ticker in the summary table
              Range("K" & summary_ticker_row).Value = percent_change
              Range("K" & summary_ticker_row).NumberFormat = "0.00%"
   
              'Reset the row counter. Add one to the summary_ticker_row
              summary_ticker_row = summary_ticker_row + 1

              'Reset volume of trade to zero
              tickervolume = 0

              'Reset the opening price
              open_price = Cells(i + 1, 3)
            
            Else
              
               'Add the volume of trade
              tickervolume = tickervolume + Cells(i, 7).Value

            
            End If
        
        Next i

    'Conditional formatting that will highlight positive change in green and negative change in red
    'First find the last row of the summary table

    lastrow_summary_table = Cells(Rows.Count, 9).End(xlUp).Row
    
    'Color code yearly change
    
    For i = 2 To lastrow_summary_table
            If Cells(i, 10).Value > 0 Then
                Cells(i, 10).Interior.ColorIndex = 10
            Else
                Cells(i, 10).Interior.ColorIndex = 3
            End If
    Next i
        Next ws
    
    

End Sub
