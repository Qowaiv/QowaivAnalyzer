Imports System
Imports Microsoft.VisualBasic

Namespace QowaivAnalyzer.UnitTests.TestCases

    Public Class UseInjectableDateTimeProvider

        Public Sub Test()
            Dim act As Date = Date.UtcNow
            Dim exp As New Date(2017, 6, 11)
            Dim other As Date = UtcNow
            Dim today As Date = Date.Today
            Dim now_ As Date = Now
        End Sub

        Public ReadOnly Property UtcNow As Date
            Get
                Return DateTime.Now
            End Get
        End Property
    End Class

End Namespace
