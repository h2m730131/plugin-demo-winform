Public Class HannStar
    Inherits MarshalByRefObject
    Implements COM.IShowName


    Public Function Show() As String Implements COM.IShowName.Show
        Return "瀚宇彩晶"
    End Function

End Class
