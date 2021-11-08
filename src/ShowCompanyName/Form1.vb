Imports System.Reflection


Public Class Form1

    Private Sub btnShow_Click(sender As System.Object, e As System.EventArgs) Handles btnShow.Click
        '*** .Net無法釋放已經使用的DLL, 但是可以卸載AppDomain, 所以建立新的AppDomain並載入DLL,
        '*** 然後在卸載新的AppDomain, 也就會一併釋放新的AppDomain的DLL


        Dim strDllFile As String = ".\HannStar.dll" 'DLL檔案的路徑和名稱
        Dim strClsFullName As String = "HannStar.HannStar" 'Class的完整名稱 ([Namespace].[Class Name])

        Dim ads As New AppDomainSetup()
        '設定成載入DLL檔案 (A) 前, 把DLL檔案複製到另一個位置 (B), 從此位置載入DLL檔案 (B),
        '這樣就可以隨時隨地的取代此DLL檔案 (A)
        ads.ShadowCopyFiles = "true"

        '另外建立一個新的AppDomain來載入DLL, 以達到釋放DLL的目的
        Dim ad As AppDomain = AppDomain.CreateDomain("New AppDomain (HannStar)", Nothing, ads)

        '1. 讓新的AppDomain來載入DLL, 並建立Class,
        '   因為是在新的AppDomain建立Class給目前的AppDomain, 有跨AppDomain的情形, 所以Class要繼承MarshalByRefObject
        '2. 宣告成COM.IShowName, 借由第3方來存取,
        '   這是為了要避免目前的AppDomain使用到DLL的Class, 才不會被目前的AppDomain給鎖定此DLL
        Dim objShowName As COM.IShowName = ad.CreateInstanceFromAndUnwrap(strDllFile, strClsFullName)


        MessageBox.Show(objShowName.Show())
        AppDomain.Unload(ad) '卸載AppDomain, 也就釋放了DLL
    End Sub

End Class
