Public Class HannStar
    Inherits MarshalByRefObject
    Implements COM.IShowName


    Public Function Show() As String Implements COM.IShowName.Show
        Return "HannStar"
    End Function

End Class
