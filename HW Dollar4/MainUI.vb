Imports Microsoft.Win32
Imports System.IO
Imports System.IO.Compression
Imports System.Media
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography
Imports System.Text

Public Class MainUI

    <DllImport("libmbedtls.dll", CallingConvention:=CallingConvention.Cdecl)>
    Public Shared Function libmbedtls_gcm_auth_decrypt_pbkdf2(password As Byte(), password_len As UInteger, salt As Byte(), salt_len As UInteger, iv As Byte(), tag As Byte(), cipher_data As Byte(), data_len As UInteger, decrypt_data As Byte()) As Integer
    End Function

    <DllImport("libmbedtls.dll", CallingConvention:=CallingConvention.Cdecl)>
    Public Shared Function libmbedtls_gcm_crypt_and_tag_pbkdf2(password As Byte(), password_len As UInteger, salt As Byte(), salt_len As UInteger, data_len As UInteger, iv As Byte(), input_data As Byte(), cipher_data As Byte(), tag As Byte()) As Integer
    End Function

    <DllImport("libmbedtls.dll", CallingConvention:=CallingConvention.Cdecl)>
    Public Shared Function libmbedtls_md_hmac_sha256(key As Byte(), key_len As UInteger, data As Byte(), data_len As UInteger, hmac_result As Byte()) As Integer
    End Function

    <DllImport("libmbedtls.dll", CallingConvention:=CallingConvention.Cdecl)>
    Public Shared Function libmbedtls_test_add(a As Integer, b As Integer) As Integer
    End Function

    <DllImport("libmbedtls.dll", CallingConvention:=CallingConvention.Cdecl)>
    Public Shared Function libmbedtls_test_subtract(a As Integer, b As Integer) As Integer
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function AppendMenu(hMenu As IntPtr, uFlags As MenuFlags, uIDNewItem As Integer, lpNewItem As String) As Boolean
    End Function

    <DllImport("user32.dll", CallingConvention:=CallingConvention.Cdecl)>
    Public Shared Function GetSystemMenu(hWnd As IntPtr, Optional bRevert As Boolean = False) As IntPtr
    End Function

    <Flags()>
    Public Enum MenuFlags As Integer
        MF_BYPOSITION = 1024
        MF_REMOVE = 4096
        MF_SEPARATOR = 2048
        MF_STRING = 0
    End Enum

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = 274 Then
            If m.WParam.ToInt32 = &H1FFE Then
                MsgBox("此软件包含的开源软件由权利持有人发放许可证。" & vbCrLf & vbCrLf & "软件： aescrypt2 1.0" & vbCrLf & "版权声明： Copyright (C) 2004,2005  Christophe Devine" & vbCrLf & "许可证： GPL v2 License" & vbCrLf & "许可证文本： https://www.gnu.org/licenses/gpl-2.0.txt" & vbCrLf & vbCrLf & "软件： Mbed TLS 3.6.0 LTS" & vbCrLf & "版权声明： (C) Copyright The Mbed TLS Contributors" & vbCrLf & "许可证： GPL v2 License" & vbCrLf & "许可证文本： https://www.gnu.org/licenses/gpl-2.0.txt", vbInformation, "开源软件使用声明")
            End If

            If m.WParam.ToInt32 = &H1FFF Then
                Dim VersionStrings As String() = Application.ProductVersion.ToString.Split(".")
                MsgBox("HW Dollar4" & vbCrLf & vbCrLf & "华为ONT配置文件实用工具" & vbCrLf & vbCrLf & "软件版本：" & VersionStrings(0) & "." & VersionStrings(1) & "." & VersionStrings(2) & vbCrLf & "更新时间：20" & VersionStrings(3).Substring(0, 2) & "年" & Int(VersionStrings(3).Substring(2, 2)) & "月" & vbCrLf & vbCrLf & "Copyright © 2020-20" & VersionStrings(3).Substring(0, 2) & " 版权所有", vbInformation, "关于")
            End If
        End If
        MyBase.WndProc(m)
    End Sub

    Public Shared Aescrypt2_EXE As String = Application.StartupPath & "\aescrypt2.exe"
    Public Shared BoardInfoVersion As Integer = 3
    Public Shared D4KeyStore As Byte() = New Byte() {}
    Public Shared D4KeyStoreFile As Byte() = New Byte() {}
    Public Shared D4KeyStoreLast As String = Application.StartupPath
    Public Shared D4Password As Byte() = New Byte() {}
    Public Shared D4PasswordLast As String = ""
    Public ReadOnly HW_CTREE_Key As String = "hex:13395537D2730554A176799F6D56A239" 'Keystore D4K1
    Public ReadOnly HW_D1_Key As Byte() = HexToBytes("B8363C9B77DAED4B9ABB9F2F6DF5F1D5CB64975D5D3BCEE8827F2F42235F9229") 'Keystore D1K1
    Public ReadOnly HW_D2_Key As Byte() = Encoding.UTF8.GetBytes("9jK0lk5kLmxn8sjojW962llHY76xAc2z") 'Keystore D1K2
    Public Shared PolicyPassword As String = ""
    Public Shared SearchText As String = ""

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.F) Then
            Try
                SearchText = ""
                Search.ShowDialog()
                If SearchText.Length > 0 Then
                    Dim _loc_1 As Integer = TxtMain.Text.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase)
                    If _loc_1 <> -1 Then
                        TxtMain.Select(_loc_1, SearchText.Length)
                        TxtMain.Focus()
                        TxtMain.ScrollToCaret()
                    Else
                        SystemSounds.Hand.Play()
                    End If
                End If
                SearchText = ""
            Catch ex As Exception

            End Try
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Public Function AesCrypt2(param1 As Byte(), param2 As Integer, param3 As Byte()) As Byte()
        Dim TempPath As String = Path.GetTempPath()
        Dim InputFile As String = Path.GetTempFileName()
        Dim OutputFile As String = Path.GetTempFileName()
        Dim KeyFile As String = Path.GetTempFileName()
        My.Computer.FileSystem.WriteAllBytes(InputFile, param1, False)
        My.Computer.FileSystem.WriteAllBytes(KeyFile, param3, False)
        Dim _loc_1 As New Process()
        _loc_1.StartInfo.FileName = Aescrypt2_EXE
        _loc_1.StartInfo.Arguments = Chr(34) & param2.ToString() & Chr(34) & " " & Chr(34) & InputFile.Replace("\", "/") & Chr(34) & " " & Chr(34) & OutputFile.Replace("\", "/") & Chr(34) & " " & Chr(34) & KeyFile & Chr(34)
        _loc_1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        _loc_1.Start()
        _loc_1.WaitForExit(10000)
        Try
            _loc_1.Kill()
        Catch ex As Exception

        End Try
        _loc_1.Close()
        Dim _loc_2 As Byte() = My.Computer.FileSystem.ReadAllBytes(OutputFile)
        My.Computer.FileSystem.DeleteFile(InputFile)
        My.Computer.FileSystem.DeleteFile(KeyFile)
        My.Computer.FileSystem.DeleteFile(OutputFile)
        Return _loc_2
    End Function

    Public Function BinToBytes(param1 As String) As Byte()
        Dim _loc_1 As Integer = param1.Length / 8
        Dim _loc_2 As Byte() = New Byte(_loc_1 - 1) {}
        For _loc_3 As Integer = 0 To _loc_1 - 1
            _loc_2(_loc_3) = Convert.ToByte(param1.Substring(8 * _loc_3, 8), 2)
        Next
        Return _loc_2
    End Function

    Private Sub BtnCopy_Click(sender As Object, e As EventArgs) Handles BtnCopy.Click
        Try
            If TxtMain.TextLength > 0 Then
                Clipboard.SetText(TxtMain.Text)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Try
            If SFDBoardinfo.ShowDialog = DialogResult.OK Then
                Dim SaveBuffer As Byte() = Encoding.UTF8.GetBytes(TxtMain.Text)
                Dim SaveBufferHandled As Boolean = False
                Dim SaveBufferInvalid As Boolean = False

                If RadD4.Checked Then
                    If SaveBuffer(0) = &H3C Then
                        If ChkPolicy.Checked Then
                            D4KeyStore = New Byte(3) {}
                            Dim _loc_11 As Byte() = HW_D4EncryptXML(TxtMain.Text)
                            HW_D4Reset()
                            Dim _loc_12(_loc_11.Length + 327) As Byte
                            _loc_12(0) = &H7
                            _loc_12(1) = &H12
                            _loc_12(2) = &H21
                            _loc_12(3) = &H20

                            _loc_12(52) = &H48
                            _loc_12(53) = &H1

                            Dim _loc_13 As Byte() = BitConverter.GetBytes(Convert.ToInt64(_loc_11.Length))
                            _loc_12(56) = _loc_13(0)
                            _loc_12(57) = _loc_13(1)
                            _loc_12(58) = _loc_13(2)
                            _loc_12(59) = _loc_13(3)

                            _loc_12(96) = D4KeyStore(1)
                            _loc_12(97) = D4KeyStore(0)
                            _loc_12(100) = D4KeyStore(3)
                            _loc_12(101) = D4KeyStore(2)

                            _loc_12(60) = &H1
                            Dim _loc_4(39) As Byte
                            Array.Copy(_loc_12, 64, _loc_4, 0, _loc_4.Length)
                            Dim _loc_5 As Byte() = BitConverter.GetBytes(CRC32(_loc_4))
                            Dim _loc_6 As Byte() = BitConverter.GetBytes(CRC32(_loc_11))

                            _loc_12(320) = _loc_6(0)
                            _loc_12(321) = _loc_6(1)
                            _loc_12(322) = _loc_6(2)
                            _loc_12(323) = _loc_6(3)

                            _loc_12(324) = _loc_5(0)
                            _loc_12(325) = _loc_5(1)
                            _loc_12(326) = _loc_5(2)
                            _loc_12(327) = _loc_5(3)

                            Array.Copy(_loc_11, 0, _loc_12, 328, _loc_11.Length)

                            My.Computer.FileSystem.WriteAllBytes(SFDBoardinfo.FileName, _loc_12, False)
                            TxtMain.Text = "[ 提示 ] 文件已保存！" & vbCrLf & SFDBoardinfo.FileName
                            Exit Sub
                        Else
                            SaveBuffer = HW_D4EncryptXML(TxtMain.Text)
                        End If
                        SaveBufferHandled = True
                    Else
                        If BoardInfoVersion = 3 Then BoardInfoVersion = 5
                        ChkPolicy.Checked = False
                    End If
                Else
                    ChkPolicy.Checked = False
                End If

                If (BoardInfoVersion = 5 Or BoardInfoVersion = 6) And Not SaveBufferHandled Then
                    If Not ((TxtMain.Text.StartsWith("$2") Or TxtMain.Text.StartsWith("$4")) And TxtMain.Text.EndsWith("$")) Then
                        If ParseText() Then
                            SaveBuffer = Encoding.UTF8.GetBytes(TxtMain.Text)
                        Else
                            SaveBufferInvalid = True
                        End If
                    End If

                    Dim HeadBuffer As Byte() = New Byte(15) {}
                    If BoardInfoVersion = 5 Then
                        HeadBuffer(0) = &H1B
                        HeadBuffer(1) = &H5C
                        HeadBuffer(2) = &H9F
                        HeadBuffer(3) = &H3A
                        HeadBuffer(4) = &H12
                        HeadBuffer(5) = &H3
                        HeadBuffer(6) = &H20
                        HeadBuffer(7) = &H20
                    ElseIf BoardInfoVersion = 6 Then
                        HeadBuffer(0) = &H3A
                        HeadBuffer(1) = &H9F
                        HeadBuffer(2) = &H5C
                        HeadBuffer(3) = &H1B
                        HeadBuffer(4) = &H20
                        HeadBuffer(5) = &H20
                        HeadBuffer(6) = &H3
                        HeadBuffer(7) = &H12
                    End If

                    Dim _loc_1 As Byte() = BitConverter.GetBytes(Convert.ToInt64(SaveBuffer.Length))
                    If BoardInfoVersion = 5 Then
                        HeadBuffer(8) = _loc_1(0)
                        HeadBuffer(9) = _loc_1(1)
                        HeadBuffer(10) = _loc_1(2)
                        HeadBuffer(11) = _loc_1(3)
                    ElseIf BoardInfoVersion = 6 Then
                        HeadBuffer(8) = _loc_1(3)
                        HeadBuffer(9) = _loc_1(2)
                        HeadBuffer(10) = _loc_1(1)
                        HeadBuffer(11) = _loc_1(0)
                    End If

                    Dim _loc_2 As Byte() = BitConverter.GetBytes(CRC32(SaveBuffer))
                    If BoardInfoVersion = 5 Then
                        HeadBuffer(12) = _loc_2(0)
                        HeadBuffer(13) = _loc_2(1)
                        HeadBuffer(14) = _loc_2(2)
                        HeadBuffer(15) = _loc_2(3)
                    ElseIf BoardInfoVersion = 6 Then
                        HeadBuffer(12) = _loc_2(3)
                        HeadBuffer(13) = _loc_2(2)
                        HeadBuffer(14) = _loc_2(1)
                        HeadBuffer(15) = _loc_2(0)
                    End If

                    Dim DataBuffer As Byte() = New Byte(SaveBuffer.Length + 19) {}
                    Array.Copy(HeadBuffer, 0, DataBuffer, 0, HeadBuffer.Length)
                    Dim _loc_3 As Byte() = BitConverter.GetBytes(CRC32(HeadBuffer))
                    If BoardInfoVersion = 5 Then
                        DataBuffer(16) = _loc_3(0)
                        DataBuffer(17) = _loc_3(1)
                        DataBuffer(18) = _loc_3(2)
                        DataBuffer(19) = _loc_3(3)
                    ElseIf BoardInfoVersion = 6 Then
                        DataBuffer(16) = _loc_3(3)
                        DataBuffer(17) = _loc_3(2)
                        DataBuffer(18) = _loc_3(1)
                        DataBuffer(19) = _loc_3(0)
                    End If

                    Array.Copy(SaveBuffer, 0, DataBuffer, 20, SaveBuffer.Length)
                    SaveBuffer = DataBuffer
                End If

                If SaveBufferInvalid Then
                    TxtMain.Text = "[ 提示 ] 文件保存失败！"
                Else
                    Dim hmac_result(31) As Byte
                    libmbedtls_md_hmac_sha256(HW_D2_Key, HW_D2_Key.Length, SaveBuffer, SaveBuffer.Length, hmac_result)
                    My.Computer.FileSystem.WriteAllBytes(SFDBoardinfo.FileName, SaveBuffer, False)
                    My.Computer.FileSystem.WriteAllBytes(SFDBoardinfo.FileName & ".hash", hmac_result, False)
                    TxtMain.Text = "[ 提示 ] 文件已保存！" & vbCrLf & SFDBoardinfo.FileName & vbCrLf & SFDBoardinfo.FileName & ".hash"
                End If
            End If
        Catch ex As Exception
            TxtMain.Text = "[ 提示 ] 文件保存失败！"
        End Try
    End Sub

    Private Sub BtnOpen_Click(sender As Object, e As EventArgs) Handles BtnOpen.Click
        Try
            TxtMain.Clear()
            BoardInfoVersion = 3
            If OfdBoardinfo.ShowDialog = DialogResult.OK Then
                LoadConfig(OfdBoardinfo.FileName)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnParse_Click(sender As Object, e As EventArgs) Handles BtnParse.Click
        Try
            ParseText()
        Catch ex As Exception

        End Try
    End Sub

    Public Function BytesToBin(param1() As Byte) As String
        Dim _loc_1 As New StringBuilder
        For Each _loc_2 In param1
            _loc_1.Append(Convert.ToString(_loc_2, 2).PadLeft(8, "0"))
        Next
        Return _loc_1.ToString
    End Function

    Public Function BytesToHex(param1 As Byte()) As String
        Return BitConverter.ToString(param1).Replace("-", "").ToUpper
    End Function

    Public Function BytesToInt32(param1 As Byte()) As Integer
        Return Int(param1(0)) + Int(param1(1)) * 256 + Int(param1(2)) * 65536 + Int(param1(3)) * 16777216
    End Function

    Public Function CRC32(param1 As Byte()) As UInteger
        Dim _loc_1 As UInteger() = New UInteger(255) {}
        Dim _loc_2 As UInteger = &HEDB88320UI
        For _loc_3 As UInteger = 0 To _loc_1.Length - 1
            Dim _loc_4 As UInteger = _loc_3
            For _loc_5 As Integer = 8 To 1 Step -1
                If (_loc_4 And 1) = 1 Then
                    _loc_4 = (_loc_4 >> 1) Xor _loc_2
                Else
                    _loc_4 >>= 1
                End If
            Next
            _loc_1(_loc_3) = _loc_4
        Next
        Dim _loc_6 As UInteger = &HFFFFFFFFUI
        For _loc_7 As Integer = 0 To param1.Length - 1
            Dim _loc_8 As Byte = ((_loc_6) And &HFF) Xor param1(_loc_7)
            _loc_6 = (_loc_6 >> 8) Xor _loc_1(_loc_8)
        Next
        Return Not _loc_6
    End Function

    Private Function DecryptAES(Input As Byte(), Key As Byte(), IV As Byte()) As Byte()
        Dim Decryptor As System.Security.Cryptography.Aes = System.Security.Cryptography.Aes.Create("AES")
        Decryptor.BlockSize = 128
        Decryptor.KeySize = 256
        Decryptor.Key = Key
        Decryptor.IV = IV
        Decryptor.Mode = CipherMode.CBC
        Decryptor.Padding = PaddingMode.Zeros
        Return Decryptor.CreateDecryptor().TransformFinalBlock(Input, 0, Input.Length)
    End Function

    Private Function EncryptAES(Input As Byte(), Key As Byte(), IV As Byte()) As Byte()
        Dim Encryptor As System.Security.Cryptography.Aes = System.Security.Cryptography.Aes.Create("AES")
        Encryptor.BlockSize = 128
        Encryptor.KeySize = 256
        Encryptor.Key = Key
        Encryptor.IV = IV
        Encryptor.Mode = CipherMode.CBC
        Encryptor.Padding = PaddingMode.Zeros
        Return Encryptor.CreateEncryptor().TransformFinalBlock(Input, 0, Input.Length)
    End Function

    Private Function GetBigEndian32(param1 As Byte(), param2 As Integer) As UInteger
        Dim _loc_1(3) As Byte
        Array.Copy(param1, param2, _loc_1, 0, 4)
        If BitConverter.IsLittleEndian Then Array.Reverse(_loc_1)
        Return BitConverter.ToUInt32(_loc_1, 0)
    End Function

    Public Function GetEnvironmentVersion() As Integer
        Try
            Dim _loc_1 As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full", False)
            If _loc_1 IsNot Nothing Then
                Return Int(_loc_1.GetValue("Release", "0"))
            End If
            Return 0
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function GetLittleEndian32(param1 As Byte(), param2 As Integer) As UInteger
        Dim _loc_1(3) As Byte
        Array.Copy(param1, param2, _loc_1, 0, 4)
        If Not BitConverter.IsLittleEndian Then Array.Reverse(_loc_1)
        Return BitConverter.ToUInt32(_loc_1, 0)
    End Function

    Public Function GZip(param1 As Byte()) As Byte()
        Dim _loc_1 As Byte() = param1
        Dim _loc_2 As New MemoryStream()
        Using _loc_3 As New Compression.GZipStream(_loc_2, CompressionMode.Compress, True)
            _loc_3.Write(_loc_1, 0, _loc_1.Length)
        End Using
        _loc_2.Position = 0
        Dim _loc_4 As Byte() = New Byte(_loc_2.Length - 1) {}
        _loc_2.Read(_loc_4, 0, _loc_4.Length)
        Return _loc_4
    End Function

    Public Function HexToBytes(param1 As String) As Byte()
        Return Enumerable.Range(0, param1.Length).Where(Function(x) x Mod 2 = 0).[Select](Function(x) Convert.ToByte(param1.Substring(x, 2), 16)).ToArray()
    End Function

    Public Function HW_BytesToStr(param1 As Byte()) As String
        Dim _loc_7 As Byte() = New Byte(19) {}
        For _loc_8 = 0 To 3
            Dim _loc_9 As Long = BitConverter.ToUInt32(New Byte() {param1(_loc_8 * 4), param1(_loc_8 * 4 + 1), param1(_loc_8 * 4 + 2), param1(_loc_8 * 4 + 3)}, 0)
            Dim _loc_10 As Long = 74805201
            For _loc_11 = 4 To 0 Step -1
                _loc_7(_loc_8 * 5 + _loc_11) = Math.Floor(_loc_9 / _loc_10)
                _loc_9 = _loc_9 Mod _loc_10
                _loc_10 /= 93
            Next
        Next
        For _loc_12 = 0 To 19
            If _loc_7(_loc_12) = 30 Then
                _loc_7(_loc_12) = 126
            Else
                _loc_7(_loc_12) += 33
            End If
        Next
        Return Encoding.UTF8.GetString(_loc_7)
    End Function

    ReadOnly HW_CTREE_CRC_Table As UInteger() = {&H0UI, &H4C11DB7UI, &H9823B6EUI, &HD4326D9UI, &H130476DCUI, &H17C56B6BUI, &H1A864DB2UI, &H1E475005UI, &H2608EDB8UI, &H22C9F00FUI, &H2F8AD6D6UI, &H2B4BCB61UI, &H350C9B64UI, &H31CD86D3UI, &H3C8EA00AUI, &H384FBDBDUI, &H4C11DB70UI, &H48D0C6C7UI, &H4593E01EUI, &H4152FDA9UI, &H5F15ADACUI, &H5BD4B01BUI, &H569796C2UI, &H52568B75UI, &H6A1936C8UI, &H6ED82B7FUI, &H639B0DA6UI, &H675A1011UI, &H791D4014UI, &H7DDC5DA3UI, &H709F7B7AUI, &H745E66CDUI, &H9823B6E0UI, &H9CE2AB57UI, &H91A18D8EUI, &H95609039UI, &H8B27C03CUI, &H8FE6DD8BUI, &H82A5FB52UI, &H8664E6E5UI, &HBE2B5B58UI, &HBAEA46EFUI, &HB7A96036UI, &HB3687D81UI, &HAD2F2D84UI, &HA9EE3033UI, &HA4AD16EAUI, &HA06C0B5DUI, &HD4326D90UI, &HD0F37027UI, &HDDB056FEUI, &HD9714B49UI, &HC7361B4CUI, &HC3F706FBUI, &HCEB42022UI, &HCA753D95UI, &HF23A8028UI, &HF6FB9D9FUI, &HFBB8BB46UI, &HFF79A6F1UI, &HE13EF6F4UI, &HE5FFEB43UI, &HE8BCCD9AUI, &HEC7DD02DUI, &H34867077UI, &H30476DC0UI, &H3D044B19UI, &H39C556AEUI, &H278206ABUI, &H23431B1CUI, &H2E003DC5UI, &H2AC12072UI, &H128E9DCFUI, &H164F8078UI, &H1B0CA6A1UI, &H1FCDBB16UI, &H18AEB13UI, &H54BF6A4UI, &H808D07DUI, &HCC9CDCAUI, &H7897AB07UI, &H7C56B6B0UI, &H71159069UI, &H75D48DDEUI, &H6B93DDDBUI, &H6F52C06CUI, &H6211E6B5UI, &H66D0FB02UI, &H5E9F46BFUI, &H5A5E5B08UI, &H571D7DD1UI, &H53DC6066UI, &H4D9B3063UI, &H495A2DD4UI, &H44190B0DUI, &H40D816BAUI, &HACA5C697UI, &HA864DB20UI, &HA527FDF9UI, &HA1E6E04EUI, &HBFA1B04BUI, &HBB60ADFCUI, &HB6238B25UI, &HB2E29692UI, &H8AAD2B2FUI, &H8E6C3698UI, &H832F1041UI, &H87EE0DF6UI, &H99A95DF3UI, &H9D684044UI, &H902B669DUI, &H94EA7B2AUI, &HE0B41DE7UI, &HE4750050UI, &HE9362689UI, &HEDF73B3EUI, &HF3B06B3BUI, &HF771768CUI, &HFA325055UI, &HFEF34DE2UI, &HC6BCF05FUI, &HC27DEDE8UI, &HCF3ECB31UI, &HCBFFD686UI, &HD5B88683UI, &HD1799B34UI, &HDC3ABDEDUI, &HD8FBA05AUI, &H690CE0EEUI, &H6DCDFD59UI, &H608EDB80UI, &H644FC637UI, &H7A089632UI, &H7EC98B85UI, &H738AAD5CUI, &H774BB0EBUI, &H4F040D56UI, &H4BC510E1UI, &H46863638UI, &H42472B8FUI, &H5C007B8AUI, &H58C1663DUI, &H558240E4UI, &H51435D53UI, &H251D3B9EUI, &H21DC2629UI, &H2C9F00F0UI, &H285E1D47UI, &H36194D42UI, &H32D850F5UI, &H3F9B762CUI, &H3B5A6B9BUI, &H315D626UI, &H7D4CB91UI, &HA97ED48UI, &HE56F0FFUI, &H1011A0FAUI, &H14D0BD4DUI, &H19939B94UI, &H1D528623UI, &HF12F560EUI, &HF5EE4BB9UI, &HF8AD6D60UI, &HFC6C70D7UI, &HE22B20D2UI, &HE6EA3D65UI, &HEBA91BBCUI, &HEF68060BUI, &HD727BBB6UI, &HD3E6A601UI, &HDEA580D8UI, &HDA649D6FUI, &HC423CD6AUI, &HC0E2D0DDUI, &HCDA1F604UI, &HC960EBB3UI, &HBD3E8D7EUI, &HB9FF90C9UI, &HB4BCB610UI, &HB07DABA7UI, &HAE3AFBA2UI, &HAAFBE615UI, &HA7B8C0CCUI, &HA379DD7BUI, &H9B3660C6UI, &H9FF77D71UI, &H92B45BA8UI, &H9675461FUI, &H8832161AUI, &H8CF30BADUI, &H81B02D74UI, &H857130C3UI, &H5D8A9099UI, &H594B8D2EUI, &H5408ABF7UI, &H50C9B640UI, &H4E8EE645UI, &H4A4FFBF2UI, &H470CDD2BUI, &H43CDC09CUI, &H7B827D21UI, &H7F436096UI, &H7200464FUI, &H76C15BF8UI, &H68860BFDUI, &H6C47164AUI, &H61043093UI, &H65C52D24UI, &H119B4BE9UI, &H155A565EUI, &H18197087UI, &H1CD86D30UI, &H29F3D35UI, &H65E2082UI, &HB1D065BUI, &HFDC1BECUI, &H3793A651UI, &H3352BBE6UI, &H3E119D3FUI, &H3AD08088UI, &H2497D08DUI, &H2056CD3AUI, &H2D15EBE3UI, &H29D4F654UI, &HC5A92679UI, &HC1683BCEUI, &HCC2B1D17UI, &HC8EA00A0UI, &HD6AD50A5UI, &HD26C4D12UI, &HDF2F6BCBUI, &HDBEE767CUI, &HE3A1CBC1UI, &HE760D676UI, &HEA23F0AFUI, &HEEE2ED18UI, &HF0A5BD1DUI, &HF464A0AAUI, &HF9278673UI, &HFDE69BC4UI, &H89B8FD09UI, &H8D79E0BEUI, &H803AC667UI, &H84FBDBD0UI, &H9ABC8BD5UI, &H9E7D9662UI, &H933EB0BBUI, &H97FFAD0CUI, &HAFB010B1UI, &HAB710D06UI, &HA6322BDFUI, &HA2F33668UI, &HBCB4666DUI, &HB8757BDAUI, &HB5365D03UI, &HB1F740B4UI}

    Public Function HW_CTREE_CRC(param1 As Byte(), param2 As UInteger) As UInteger
        Dim _loc_6 As UInteger = param2
        For _loc_1 As Integer = 0 To param1.Length - 1
            _loc_6 = (param1(_loc_1) Or (_loc_6 << 8)) Xor HW_CTREE_CRC_Table(_loc_6 >> 24)
        Next
        For _loc_4 As Integer = 4 To 1 Step -1
            _loc_6 = HW_CTREE_CRC_Table(BitConverter.GetBytes(_loc_6)(3)) Xor (_loc_6 << 8)
        Next
        Return _loc_6
    End Function

    Public Function HW_CTREE_CRC32(param1 As Byte()) As Byte()
        Dim _loc_1 As UInteger = &H0UI
        Dim _loc_2 As Integer = 0
        While param1.Length - _loc_2 > 1024
            Dim _loc_3 As Byte() = New Byte(1024 - 1) {}
            Array.Copy(param1, _loc_2, _loc_3, 0, 1024)
            _loc_1 = HW_CTREE_CRC(_loc_3, _loc_1)
            _loc_2 += 1024
        End While
        Dim _loc_4 As Byte() = New Byte(param1.Length - _loc_2 - 1) {}
        Array.Copy(param1, _loc_2, _loc_4, 0, param1.Length - _loc_2)
        _loc_1 = HW_CTREE_CRC(_loc_4, _loc_1)
        Return BitConverter.GetBytes(_loc_1)
    End Function

    Public Function HW_D1Decrypt(param1 As String) As String
        param1 = param1.Substring(2, param1.Length - 3).Replace("&quot;", """").Replace("&amp;", "&").Replace("&apos;", "'").Replace("&lt;", "<").Replace("&gt;", ">")
        Dim _loc_1 As Integer = Math.Floor(param1.Length / 24)
        Dim _loc_2 As New List(Of String)
        For _loc_3 = 0 To _loc_1 - 1
            _loc_2.Add(param1.Substring(24 * _loc_3, 24))
        Next
        Dim _loc_5 As New List(Of Byte)()
        Dim Decryptor As System.Security.Cryptography.Aes = System.Security.Cryptography.Aes.Create("AES")
        Decryptor.BlockSize = 128
        Decryptor.KeySize = 256
        Decryptor.Mode = CipherMode.ECB
        Decryptor.Padding = PaddingMode.None
        For _loc_4 = 0 To _loc_2.Count - 1
            Decryptor.Key = HW_D1GetAESKey(_loc_2(_loc_4))
            _loc_5.AddRange(Decryptor.CreateDecryptor().TransformFinalBlock(HW_StrToBytes(_loc_2(_loc_4)), 0, 16))
        Next
        Return Encoding.UTF8.GetString(_loc_5.ToArray())
    End Function

    Public Function HW_D1Encrypt(param1 As String, Optional param2 As Boolean = False) As String
        Dim _loc_1 As Byte() = Encoding.UTF8.GetBytes(param1)
        Dim _loc_2 As Integer = Math.Ceiling(_loc_1.Length / 16)
        If _loc_2 = 0 Then _loc_2 = 1
        Dim _loc_3 As New Random
        Dim _loc_4 As Byte() = New Byte(15) {}
        Dim _loc_5 As String = ""
        Dim Encryptor As System.Security.Cryptography.Aes = System.Security.Cryptography.Aes.Create("AES")
        Encryptor.BlockSize = 128
        Encryptor.KeySize = 256
        Encryptor.Mode = CipherMode.ECB
        Encryptor.Padding = PaddingMode.None
        For _loc_6 As Integer = 0 To _loc_2 - 1
            Dim _loc_7(15) As Byte
            Dim _loc_8 As Integer = _loc_1.Length - (16 * _loc_6)
            Dim _loc_9 As Integer = If(_loc_8 > 16, 16, _loc_8)
            If _loc_9 > 0 Then Array.Copy(_loc_1, 16 * _loc_6, _loc_7, 0, _loc_9)
            _loc_3.NextBytes(_loc_4)
            Dim _loc_10 As String = HW_BytesToStr(_loc_4)
            Encryptor.Key = HW_D1GetAESKey(_loc_10)
            _loc_5 += HW_BytesToStr(Encryptor.CreateEncryptor().TransformFinalBlock(_loc_7, 0, 16)) + _loc_10.Substring(_loc_10.Length - 4, 4)
        Next
        _loc_5 = "$1" & _loc_5 & "$"
        If param2 Then
            _loc_5 = _loc_5.Replace("&", "&amp;").Replace("""", "&quot;").Replace("'", "&apos;").Replace("<", "&lt;").Replace(">", "&gt;")
        End If
        Return _loc_5
    End Function

    Public Function HW_D1GetAESKey(param1 As String) As Byte()
        Dim _loc_1 As Byte() = HW_D1_Key
        Dim _loc_2 As Byte() = Encoding.UTF8.GetBytes(param1)
        For _loc_3 = _loc_2.Length - 4 To _loc_2.Length - 1
            If _loc_2(_loc_3) = 126 Then
                _loc_2(_loc_3) = 30
            Else
                _loc_2(_loc_3) -= 33
            End If
        Next
        _loc_1(11) = _loc_2(_loc_2.Length - 4)
        _loc_1(17) = _loc_2(_loc_2.Length - 3)
        _loc_1(23) = _loc_2(_loc_2.Length - 2)
        _loc_1(29) = _loc_2(_loc_2.Length - 1)
        Return _loc_1
    End Function

    Public Function HW_D2Decrypt(param1 As String) As String
        param1 = param1.Substring(2, param1.Length - 3).Replace("&quot;", """").Replace("&amp;", "&").Replace("&apos;", "'").Replace("&lt;", "<").Replace("&gt;", ">")
        Dim _loc_1 As Integer = Math.Floor(param1.Length / 20)
        Dim _loc_2 As New List(Of String)
        For _loc_3 = 0 To _loc_1 - 1
            _loc_2.Add(param1.Substring(20 * _loc_3, 20))
        Next
        Dim _loc_5 As Byte() = New Byte((_loc_2.Count - 1) * 16 - 1) {}
        For _loc_4 = 0 To _loc_2.Count - 2
            Array.Copy(HW_StrToBytes(_loc_2(_loc_4)), 0, _loc_5, _loc_4 * 16, 16)
        Next
        Return Encoding.UTF8.GetString(DecryptAES(_loc_5, HW_D2GetAESCBCKey(), HW_StrToBytes(_loc_2(_loc_2.Count - 1))))
    End Function

    Public Function HW_D2Encrypt(param1 As String, Optional param2 As Boolean = False) As String
        Dim _loc_1 As New Random
        Dim _loc_2 As Byte() = New Byte(15) {}
        _loc_1.NextBytes(_loc_2)
        Dim _loc_3 As Byte() = EncryptAES(Encoding.UTF8.GetBytes(param1), HW_D2GetAESCBCKey(), _loc_2)
        Dim _loc_4 As Integer = Math.Floor(_loc_3.Length / 16)
        Dim _loc_5 As String = ""
        For _loc_6 = 0 To _loc_4 - 1
            Dim _loc_7 As Byte() = New Byte(15) {}
            Array.Copy(_loc_3, _loc_6 * 16, _loc_7, 0, 16)
            _loc_5 += HW_BytesToStr(_loc_7)
        Next
        _loc_5 = "$2" & _loc_5 & HW_BytesToStr(_loc_2) & "$"
        If param2 Then
            _loc_5 = _loc_5.Replace("&", "&amp;").Replace("""", "&quot;").Replace("'", "&apos;").Replace("<", "&lt;").Replace(">", "&gt;")
        End If
        Return _loc_5
    End Function

    Public Function HW_D2GetAESCBCKey() As Byte()
        Dim _loc_1 As String = CboD2.Text
        If _loc_1.Length = 0 Or _loc_1.ToUpper() = "OS" Then
            _loc_1 = "Df7!ui%s9(lmV1L8" 'SPEC_OS_AES_CBC_APP_STR from libhw_ssp_basic.so
        ElseIf _loc_1.ToUpper() = "BOARDINFO" Then
            _loc_1 = "BOARDINFO(lmV1L8" 'SPEC_BOARDINFO_AES_CBC_APP_STR from libsmp_api.so
        ElseIf _loc_1.ToUpper() = "OMCI" Then
            _loc_1 = "asdfghjkl!@#0320" 'SPEC_OMCI_AES_CBC_APP_STR from omci.elf
        End If
        Dim _loc_2 As Byte() = New Byte(HW_D2_Key.Length + _loc_1.Length - 1) {}
        Dim _loc_3 As Byte() = Encoding.UTF8.GetBytes(_loc_1)
        Array.Copy(HW_D2_Key, _loc_2, HW_D2_Key.Length)
        Array.Copy(_loc_3, 0, _loc_2, HW_D2_Key.Length, _loc_3.Length)
        Return New SHA256CryptoServiceProvider().ComputeHash(_loc_2)
    End Function

    Public Function HW_D4Decrypt(param1 As Byte()) As Byte()
        Dim password As Byte() = D4Password
        Dim DomainID As Integer = param1(8) * 256 + param1(9)
        Dim KeyID As Integer = param1(10) * 256 + param1(11)
        If Not ((DomainID = D4KeyStore(0) * 256 + D4KeyStore(1)) AndAlso (KeyID = D4KeyStore(2) * 256 + D4KeyStore(3))) AndAlso Not GetBigEndian32(D4KeyStore, 0) = 0 Then
            password = KMCv2_GetMasterKey(D4KeyStoreFile, DomainID, KeyID)
            If password.Length = 0 Then
                D4Password = New Byte() {}
                HW_D4SetPassword()
                password = D4Password
            End If
        End If
        If password.Length = 0 Then Return New Byte() {}
        Dim D4IV As Byte() = New Byte(15) {}
        Dim D4Salt As Byte() = New Byte(15) {}
        Array.Copy(param1, 8, D4KeyStore, 0, 4)
        Array.Copy(param1, 12, D4IV, 0, 16)
        Dim tag As Byte() = New Byte(15) {}
        Array.Copy(param1, 28, tag, 0, 16)
        Array.Copy(param1, 44, D4Salt, 0, 16)
        Dim cipher_data As Byte() = New Byte(param1.Length - 64 - 1) {}
        Array.Copy(param1, 64, cipher_data, 0, cipher_data.Length)
        Dim decrypt_data As Byte() = New Byte(cipher_data.Length - 1) {}
        If libmbedtls_gcm_auth_decrypt_pbkdf2(password, password.Length, D4Salt, D4Salt.Length, D4IV, tag, cipher_data, cipher_data.Length, decrypt_data) = 0 Then
            Return decrypt_data
        Else
            Return New Byte() {}
        End If
    End Function

    Public Function HW_D4DecryptString(param1 As String) As String
        Return Encoding.UTF8.GetString(HW_D4Decrypt(Convert.FromBase64String(param1.Substring(2, param1.Length - 3))))
    End Function

    Public Function HW_D4DecryptXML(param1 As Byte()) As String
        Dim _loc_1 As Byte() = New Byte(param1.Length - 8 - 1) {}
        Array.Copy(param1, 8, _loc_1, 0, _loc_1.Length)
        Return Encoding.UTF8.GetString(UnGZip(HW_D4Decrypt(_loc_1)))
    End Function

    Public Function HW_D4Encrypt(param1 As Byte()) As Byte()
        Dim _loc_1 As New Random
        Dim _loc_2 As New Random
        Dim D4IV As Byte() = New Byte(15) {}
        _loc_1.NextBytes(D4IV)
        Dim D4Salt As Byte() = New Byte(15) {}
        _loc_2.NextBytes(D4Salt)
        Dim password As Byte() = D4Password
        Dim input_data As Byte() = New Byte(param1.Length - 1) {}
        Array.Copy(param1, input_data, param1.Length)
        Dim tag As Byte() = New Byte(15) {}
        Dim cipher_data As Byte() = New Byte(param1.Length - 1) {}
        If libmbedtls_gcm_crypt_and_tag_pbkdf2(password, password.Length, D4Salt, D4Salt.Length, input_data.Length, D4IV, input_data, cipher_data, tag) = 0 Then
            Dim D4Data As Byte() = New Byte(param1.Length + 64 - 1) {}
            'Byte 1-8
            D4Data(0) = &H20
            D4Data(1) = &H22
            D4Data(2) = &H7
            D4Data(3) = &H9

            'Byte 9-60
            Array.Copy(D4KeyStore, 0, D4Data, 8, 4)
            Array.Copy(D4IV, 0, D4Data, 12, 16)
            Array.Copy(tag, 0, D4Data, 28, 16)
            Array.Copy(D4Salt, 0, D4Data, 44, 16)

            'Byte 61-64
            Dim D4DataSize As Byte() = BitConverter.GetBytes(cipher_data.Length)
            D4Data(60) = D4DataSize(3)
            D4Data(61) = D4DataSize(2)
            D4Data(62) = D4DataSize(1)
            D4Data(63) = D4DataSize(0)

            'Byte 65+
            Array.Copy(cipher_data, 0, D4Data, 64, cipher_data.Length)
            Return D4Data
        Else
            Return New Byte() {}
        End If
    End Function

    Public Function HW_D4EncryptString(param1 As String) As String
        Return "$4" & Convert.ToBase64String(HW_D4Encrypt(Encoding.UTF8.GetBytes(param1))) & "$"
    End Function

    Public Function HW_D4EncryptXML(param1 As String) As Byte()
        Dim _loc_1 As Byte() = HW_D4Encrypt(GZip(Encoding.UTF8.GetBytes(param1)))
        Dim _loc_2 As Byte() = New Byte(_loc_1.Length + 8 - 1) {}
        _loc_2(0) = &H3
        Dim _loc_3 As Byte() = HW_CTREE_CRC32(_loc_1)
        Array.Copy(_loc_3, 0, _loc_2, 4, _loc_3.Length)
        Array.Copy(_loc_1, 0, _loc_2, 8, _loc_1.Length)
        Return _loc_2
    End Function

    Public Function HW_D4SetPassword(Optional param1 As String = "") As Boolean
        Try
            If param1.Length > 1 Then
                If My.Computer.FileSystem.DirectoryExists(param1) Then D4KeyStoreLast = param1
                Dim _loc_1 As String = ""
                Dim _loc_2 As String = param1 & "\kmc_store_A"
                If Not My.Computer.FileSystem.FileExists(_loc_2) Then
                    _loc_2 = param1 & "\kmc_store_B"
                    If Not My.Computer.FileSystem.FileExists(_loc_2) Then
                        _loc_2 = Application.StartupPath & "\kmc_store_A"
                        If Not My.Computer.FileSystem.FileExists(_loc_2) Then
                            _loc_2 = Application.StartupPath & "\kmc_store_B"
                            If Not My.Computer.FileSystem.FileExists(_loc_2) Then
                                Return True
                            End If
                        End If
                    End If
                End If
                D4KeyStoreFile = My.Computer.FileSystem.ReadAllBytes(_loc_2)
                Return True
            End If
            If D4Password.Length > 0 Then Return True
            Dollar4.ShowDialog()
            If D4Password.Length > 0 Then Return True
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub HW_D4Reset()
        Try
            D4Password = HW_D2_Key
            D4KeyStore = New Byte(3) {&H0, &H1, &H0, &H2}
            D4KeyStoreFile = New Byte() {}
        Catch ex As Exception

        End Try
    End Sub

    Public Function HW_StrToBytes(param1 As String) As Byte()
        Dim _loc_5 As Byte() = Encoding.UTF8.GetBytes(param1)
        For _loc_6 = 0 To 19
            If _loc_5(_loc_6) = 126 Then
                _loc_5(_loc_6) = 30
            Else
                _loc_5(_loc_6) -= 33
            End If
        Next
        Dim _loc_7 As Byte() = New Byte(15) {}
        For _loc_8 = 0 To 3
            Dim _loc_9 As Long = 0
            Dim _loc_10 As Long = 1
            For _loc_11 = 0 To 4
                _loc_9 += _loc_10 * _loc_5(_loc_8 * 5 + _loc_11)
                _loc_10 *= 93
            Next
            Dim _loc_12 As Byte() = BitConverter.GetBytes(_loc_9)
            _loc_7(_loc_8 * 4 + 0) = _loc_12(0)
            _loc_7(_loc_8 * 4 + 1) = _loc_12(1)
            _loc_7(_loc_8 * 4 + 2) = _loc_12(2)
            _loc_7(_loc_8 * 4 + 3) = _loc_12(3)
        Next
        Return _loc_7
    End Function

    ReadOnly KSFv2_Magic As Byte() = New Byte() {&H5F, &H64, &H97, &H8D, &H19, &H4F, &H89, &HCF, &HA8, &H3F, &H8E, &HE1, &HDB, &H1, &H3C, &HC, &H88, &H42, &H4A, &H1C, &HB7, &HFC, &HAD, &H70, &H4E, &H45, &H13, &HA5, &H14, &H46, &H71, &H6C, &H0, &H2}
    ReadOnly KSFv2_Mask As Byte() = New Byte() {&HB2, &HA1, &HC, &H73, &H52, &H73, &H76, &HA1, &H60, &H62, &H2E, &H8, &H52, &H8, &H2E, &HA9, &H60, &HBC, &H2E, &H73, &H52, &HB, &HC, &HBC, &HEE, &HA, &H2E, &H8, &H52, &H9C, &H76, &HA9}

    Public Function KMCv2_GetAllMasterKey(param1 As String) As String
        Dim _loc_10 As String = "[ 提示 ] KSF 打开失败！"
        Try
            Dim KSFv2 As Byte() = My.Computer.FileSystem.ReadAllBytes(param1)
            Dim _loc_11 As String = "Domain" & vbTab & "| Key" & vbTab & "| Master Key (Hex)" & vbCrLf & "------------------------------------------------------------------------------------------------------------------------" & vbCrLf
            If KSFv2.Length < 32 Then Return _loc_10
            For _loc_1 As Integer = 0 To 33
                If KSFv2(_loc_1) <> KSFv2_Magic(_loc_1) Then Return _loc_10
            Next
            Dim KSFv2_IterationCount As Integer = GetBigEndian32(KSFv2, 52)
            Dim KSFv2_RecordCount As Integer = GetBigEndian32(KSFv2, 184)
            Dim _loc_2(31) As Byte
            Dim _loc_3(31) As Byte
            Array.Copy(KSFv2, 56, _loc_2, 0, 32)
            For _loc_4 As Integer = 0 To 31
                _loc_2(_loc_4) = KSFv2(56 + _loc_4) Xor KSFv2(88 + _loc_4) Xor KSFv2_Mask(_loc_4)
            Next
            Array.Copy(KSFv2, 152, _loc_3, 0, 32)
            Dim KSFv2_RootMasterKey As Byte() = PKCS5_PBKDF2_HMAC(_loc_2, _loc_3, KSFv2_IterationCount, 32)
            For _loc_5 As Integer = 0 To KSFv2_RecordCount - 1
                Dim _loc_6 As Integer = (_loc_5 + 1) * 256
                If _loc_6 + 256 > KSFv2.Length Then Exit For
                Dim _loc_7 As Integer = GetBigEndian32(KSFv2, _loc_6 + 0)
                Dim _loc_8 As Integer = GetBigEndian32(KSFv2, _loc_6 + 4)
                Dim MasterKey_EncryptedLength As Integer = GetBigEndian32(KSFv2, _loc_6 + 88)
                Dim MasterKey_Length As Integer = GetBigEndian32(KSFv2, _loc_6 + 92)
                Dim MasterKey_EncryptionKey(31) As Byte
                Array.Copy(KSFv2_RootMasterKey, 0, MasterKey_EncryptionKey, 0, 32)
                Dim MasterKey_EncryptionIV(15) As Byte
                Array.Copy(KSFv2, _loc_6 + 72, MasterKey_EncryptionIV, 0, 16)
                Dim MasterKey_Encrypted(MasterKey_EncryptedLength - 1) As Byte
                Array.Copy(KSFv2, _loc_6 + 96, MasterKey_Encrypted, 0, MasterKey_EncryptedLength)
                Dim _loc_9 As Byte() = DecryptAES(MasterKey_Encrypted, MasterKey_EncryptionKey, MasterKey_EncryptionIV)
                Dim MasterKey(MasterKey_Length - 1) As Byte
                Array.Copy(_loc_9, 0, MasterKey, 0, MasterKey_Length)
                _loc_11 &= _loc_7.ToString() & vbTab & "| " & _loc_8.ToString() & vbTab & "| " & BytesToHex(MasterKey) & vbCrLf
            Next
            Return _loc_11
        Catch ex As Exception
            Return _loc_10
        End Try
    End Function

    Public Function KMCv2_GetMasterKey(KSFv2 As Byte(), DomainID As Integer, KeyID As Integer) As Byte()
        Try
            If KSFv2.Length < 32 Then Return New Byte() {}
            For _loc_1 As Integer = 0 To 33
                If KSFv2(_loc_1) <> KSFv2_Magic(_loc_1) Then Return New Byte() {}
            Next
            Dim KSFv2_IterationCount As Integer = GetBigEndian32(KSFv2, 52)
            Dim KSFv2_RecordCount As Integer = GetBigEndian32(KSFv2, 184)
            Dim _loc_2(31) As Byte
            Dim _loc_3(31) As Byte
            Array.Copy(KSFv2, 56, _loc_2, 0, 32)
            For _loc_4 As Integer = 0 To 31
                _loc_2(_loc_4) = KSFv2(56 + _loc_4) Xor KSFv2(88 + _loc_4) Xor KSFv2_Mask(_loc_4)
            Next
            Array.Copy(KSFv2, 152, _loc_3, 0, 32)
            Dim KSFv2_RootMasterKey As Byte() = PKCS5_PBKDF2_HMAC(_loc_2, _loc_3, KSFv2_IterationCount, 32)
            For _loc_5 As Integer = 0 To KSFv2_RecordCount - 1
                Dim _loc_6 As Integer = (_loc_5 + 1) * 256
                If _loc_6 + 256 > KSFv2.Length Then Exit For
                Dim _loc_7 As Integer = GetBigEndian32(KSFv2, _loc_6 + 0)
                Dim _loc_8 As Integer = GetBigEndian32(KSFv2, _loc_6 + 4)
                If DomainID = _loc_7 AndAlso KeyID = _loc_8 Then
                    Dim MasterKey_EncryptedLength As Integer = GetBigEndian32(KSFv2, _loc_6 + 88)
                    Dim MasterKey_Length As Integer = GetBigEndian32(KSFv2, _loc_6 + 92)
                    Dim MasterKey_EncryptionKey(31) As Byte
                    Array.Copy(KSFv2_RootMasterKey, 0, MasterKey_EncryptionKey, 0, 32)
                    Dim MasterKey_EncryptionIV(15) As Byte
                    Array.Copy(KSFv2, _loc_6 + 72, MasterKey_EncryptionIV, 0, 16)
                    Dim MasterKey_Encrypted(MasterKey_EncryptedLength - 1) As Byte
                    Array.Copy(KSFv2, _loc_6 + 96, MasterKey_Encrypted, 0, MasterKey_EncryptedLength)
                    Dim _loc_9 As Byte() = DecryptAES(MasterKey_Encrypted, MasterKey_EncryptionKey, MasterKey_EncryptionIV)
                    Dim MasterKey(MasterKey_Length - 1) As Byte
                    Array.Copy(_loc_9, 0, MasterKey, 0, MasterKey_Length)
                    Return MasterKey
                End If
            Next
            Return New Byte() {}
        Catch ex As Exception
            Return New Byte() {}
        End Try
    End Function

    Private Sub LblVersion_Click(sender As Object, e As EventArgs) Handles LblVersion.Click
        Try
            Process.Start(Application.ExecutablePath)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadConfig(param1 As String)
        Try
            If My.Computer.FileSystem.FileExists(param1) Then
                TxtMain.Clear()
                RadV3.Checked = True
                ChkPolicy.Checked = False
                If Path.GetFileName(param1) = "kmc_store_A" OrElse Path.GetFileName(param1) = "kmc_store_B" Then
                    D4KeyStoreLast = Path.GetDirectoryName(param1)
                    TxtMain.Text = KMCv2_GetAllMasterKey(param1)
                    Exit Sub
                End If
                Dim InputBuffer As Byte() = My.Computer.FileSystem.ReadAllBytes(param1)
                If InputBuffer(0) = &H67 AndAlso InputBuffer(1) = &H66 AndAlso InputBuffer(2) = &H63 AndAlso InputBuffer(3) = &H71 Then
                    Dim _loc_31 As Byte() = New Byte(InputBuffer.Length - 33) {}
                    Array.Copy(InputBuffer, 32, _loc_31, 0, InputBuffer.Length - 32)
                    Dim _loc_32 As Byte() = New Byte(3) {}
                    Array.Copy(InputBuffer, 4, _loc_32, 0, 4)
                    Dim Ctce8PayloadCRC As String = BytesToHex(_loc_32)
                    If Not BytesToHex(BitConverter.GetBytes(CRC32(_loc_31))) = Ctce8PayloadCRC OrElse Not BitConverter.ToInt32(InputBuffer, 8) = _loc_31.Length Then
                        TxtMain.Text = "[ 提示 ] CFG配置文件CRC检查失败！"
                        Exit Sub
                    End If
                    InputBuffer = UnGZip(_loc_31)
                ElseIf InputBuffer(0) = &H7 AndAlso InputBuffer(1) = &H12 AndAlso InputBuffer(2) = &H21 AndAlso InputBuffer(3) = &H20 Then
                    Dim _loc_2 As Integer = GetLittleEndian32(InputBuffer, 52)
                    If _loc_2 = 328 Then
                        Dim _loc_10 As Integer = GetLittleEndian32(InputBuffer, 56)
                        Dim _loc_11(_loc_10 - 1) As Byte
                        Array.Copy(InputBuffer, 328, _loc_11, 0, _loc_10)
                        Dim _loc_12(39) As Byte
                        Array.Copy(InputBuffer, 64, _loc_12, 0, _loc_12.Length)
                        If GetLittleEndian32(InputBuffer, 60) = 1 AndAlso GetLittleEndian32(InputBuffer, 320) = CRC32(_loc_11) AndAlso GetLittleEndian32(InputBuffer, 324) = CRC32(_loc_12) Then
                            If GetLittleEndian32(InputBuffer, 328) = 3 Then
                                Dim _loc_3 As Integer = GetLittleEndian32(InputBuffer, 4)
                                PolicyPassword = ""
                                Policy.ShowDialog()
                                If PolicyPassword.Length = 0 Then
                                    D4KeyStore = New Byte(3) {}
                                Else
                                    If _loc_3 = 1 Then
                                        D4Password = Encoding.UTF8.GetBytes(PolicyPassword)
                                        D4KeyStore = New Byte(3) {}
                                    ElseIf _loc_3 = 2 Then
                                        Dim _loc_4 As Integer = Array.IndexOf(InputBuffer, CByte(0), 12) - 12
                                        Dim _loc_5(_loc_4 - 1) As Byte
                                        Array.Copy(InputBuffer, 12, _loc_5, 0, _loc_4)
                                        D4Password = Encoding.UTF8.GetBytes(BytesToHex(PKCS5_PBKDF2_HMAC(Encoding.UTF8.GetBytes(PolicyPassword), _loc_5, 10000, 16)).ToLower())
                                        D4KeyStore = New Byte(3) {}
                                    Else
                                        D4KeyStore = New Byte(3) {}
                                    End If
                                End If
                                TxtMain.Text = HW_D4DecryptXML(_loc_11)
                                RadD4.Checked = True
                                ChkPolicy.Checked = True
                                PolicyPassword = ""
                                HW_D4Reset()
                                Exit Sub
                            Else
                                ChkPolicy.Checked = True
                                InputBuffer = _loc_11
                            End If
                        Else
                            TxtMain.Text = "[ 提示 ] 下载配置文件CRC检查失败！"
                            Exit Sub
                        End If
                    Else
                        TxtMain.Text = "[ 提示 ] 文件打开失败！"
                        Exit Sub
                    End If
                End If
                If InputBuffer(0) = &H1B AndAlso InputBuffer(1) = &H5C AndAlso InputBuffer(2) = &H9F AndAlso InputBuffer(3) = &H3A AndAlso InputBuffer(4) = &H12 AndAlso InputBuffer(5) = &H3 AndAlso InputBuffer(6) = &H20 AndAlso InputBuffer(7) = &H20 Then
                    RadV5.Checked = True
                ElseIf InputBuffer(3) = &H1B AndAlso InputBuffer(2) = &H5C AndAlso InputBuffer(1) = &H9F AndAlso InputBuffer(0) = &H3A AndAlso InputBuffer(7) = &H12 AndAlso InputBuffer(6) = &H3 AndAlso InputBuffer(5) = &H20 AndAlso InputBuffer(4) = &H20 Then
                    RadV6.Checked = True
                ElseIf InputBuffer(0) = &H20 AndAlso InputBuffer(1) = &H22 AndAlso InputBuffer(2) = &H7 AndAlso InputBuffer(3) = &H9 Then
                    HW_D4SetPassword(Path.GetDirectoryName(param1))
                    Dim D4DecryptBuffer As Byte() = HW_D4Decrypt(InputBuffer)
                    If D4DecryptBuffer.Length > 0 Then
                        RenameConfig(param1)
                        My.Computer.FileSystem.WriteAllBytes(param1, D4DecryptBuffer, False)
                        TxtMain.Text = "[ 提示 ] 文件已保存！" & vbCrLf & param1
                        RadD4.Checked = True
                    Else
                        TxtMain.Text = "[ 提示 ] 文件打开失败！"
                    End If
                    HW_D4Reset()
                    Exit Sub
                ElseIf InputBuffer(0) = &H3 AndAlso InputBuffer(1) = &H0 AndAlso InputBuffer(2) = &H0 AndAlso InputBuffer(3) = &H0 Then
                    Dim HeaderLength As Integer = 8
                    Dim _loc_9 As Byte() = New Byte(3) {}
                    Array.Copy(InputBuffer, 4, _loc_9, 0, 4)
                    Dim PayloadCRC As String = BytesToHex(_loc_9)
                    Dim Payload As Byte() = New Byte(InputBuffer.Length - HeaderLength - 1) {}
                    Array.Copy(InputBuffer, HeaderLength, Payload, 0, InputBuffer.Length - HeaderLength)
                    If Not BytesToHex(HW_CTREE_CRC32(Payload)) = PayloadCRC Then
                        TxtMain.Text = "[ 提示 ] XML配置文件CRC检查失败！"
                        Exit Sub
                    End If
                    HW_D4SetPassword(Path.GetDirectoryName(param1))
                    TxtMain.Text = HW_D4DecryptXML(InputBuffer)
                    RadD4.Checked = True
                    HW_D4Reset()
                    Exit Sub
                ElseIf (InputBuffer(0) = &H1 OrElse InputBuffer(0) = &H2) AndAlso InputBuffer(1) = &H0 AndAlso InputBuffer(2) = &H0 AndAlso InputBuffer(3) = &H0 Then
                    Dim _loc_1 As Byte() = New Byte(3) {}
                    Array.Copy(InputBuffer, 4, _loc_1, 0, 4)
                    Dim PayloadCRC As String = BytesToHex(_loc_1)

                    Dim HeaderLength As Integer = 8
                    Dim DecryptKey As String = HW_CTREE_Key
                    If InputBuffer(0) = &H2 Then
                        Array.Copy(InputBuffer, 8, _loc_1, 0, 4)
                        Dim DecryptKeyLength As Integer = BytesToInt32(_loc_1)
                        Dim DecryptKeyBuffer As Byte() = New Byte(DecryptKeyLength - 1) {}
                        Array.Copy(InputBuffer, 12, DecryptKeyBuffer, 0, DecryptKeyLength)
                        DecryptKey = HW_D2Decrypt(Encoding.UTF8.GetString(DecryptKeyBuffer))
                        HeaderLength = 12 + DecryptKeyLength
                    End If

                    Dim Payload As Byte() = New Byte(InputBuffer.Length - HeaderLength - 1) {}
                    Array.Copy(InputBuffer, HeaderLength, Payload, 0, InputBuffer.Length - HeaderLength)
                    If Not BytesToHex(HW_CTREE_CRC32(Payload)) = PayloadCRC Then
                        TxtMain.Text = "[ 提示 ] XML配置文件CRC检查失败！"
                        Exit Sub
                    End If

                    Dim DecryptBuffer As Byte() = AesCrypt2(Payload, 1, Encoding.UTF8.GetBytes(DecryptKey))
                    If DecryptBuffer(0) = &H1F AndAlso DecryptBuffer(1) = &H8B Then
                        Dim UnzipBuffer As Byte() = UnGZip(DecryptBuffer)
                        TxtMain.Text = Encoding.UTF8.GetString(UnzipBuffer)
                    ElseIf DecryptBuffer.Length > 0 Then
                        TxtMain.Text = Encoding.UTF8.GetString(DecryptBuffer)
                    Else
                        TxtMain.Text = "[ 提示 ] 文件打开失败！"
                    End If
                    Exit Sub
                End If

                If BoardInfoVersion = 5 OrElse BoardInfoVersion = 6 Then
                    Dim DataBuffer As Byte() = New Byte(InputBuffer.Length - 21) {}
                    Array.Copy(InputBuffer, 20, DataBuffer, 0, InputBuffer.Length - 20)
                    InputBuffer = DataBuffer
                End If
                TxtMain.Text = Encoding.UTF8.GetString(InputBuffer)
                If BoardInfoVersion = 5 OrElse BoardInfoVersion = 6 Then
                    ParseText(Path.GetDirectoryName(param1))
                End If
            End If
        Catch ex As Exception
            TxtMain.Text = "[ 提示 ] 文件打开失败！"
            HW_D4Reset()
        End Try
    End Sub

    Private Sub MainUI_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
        Try
            LoadConfig(e.Data.GetData(DataFormats.FileDrop)(0))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MainUI_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        Try
            If e.Data.GetDataPresent(DataFormats.FileDrop) = True Then
                e.Effect = DragDropEffects.Copy
            Else
                e.Effect = DragDropEffects.None
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MainUI_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim VersionStrings As String() = Application.ProductVersion.ToString.Split(".")
            LblVersion.Text = "HW Dollar4 版本: " & VersionStrings(0) & "." & VersionStrings(1) & "." & VersionStrings(2) & " (20" & VersionStrings(3).Substring(0, 2) & "." & VersionStrings(3).Substring(2, 2) & ")"

            If GetEnvironmentVersion() < 461808 Or LblVersion.Font.Size > 12 Then
                MsgBox("若要运行此应用程序，您必须首先安装 .NET Framework 的以下版本之一:" & vbCrLf & " v4.7.2", vbCritical, "HW Dollar4 - .NET Framework 初始化错误")
                End
            End If

            If Not My.Computer.FileSystem.FileExists(Application.StartupPath & "\aescrypt2.exe") Then
                MsgBox("由于找不到 aescrypt2.exe，无法继续执行代码。重新安装程序可能会解决此问题。", vbCritical, "HW Dollar4 - 系统错误")
                End
            End If

            If Not My.Computer.FileSystem.FileExists(Application.StartupPath & "\libmbedtls.dll") Then
                MsgBox("由于找不到 libmbedtls.dll，无法继续执行代码。重新安装程序可能会解决此问题。", vbCritical, "HW Dollar4 - 系统错误")
                End
            End If

            AppendMenu(GetSystemMenu(Handle), MenuFlags.MF_SEPARATOR, &H1FFD, "SEPARATOR")
            AppendMenu(GetSystemMenu(Handle), MenuFlags.MF_STRING, &H1FFE, "开源软件使用声明(&O)")
            AppendMenu(GetSystemMenu(Handle), MenuFlags.MF_STRING, &H1FFF, "关于(&A)")
        Catch ex As Exception

        End Try
    End Sub

    Private Function ParseText(Optional param1 As String = "") As Boolean
        Try
            If TxtMain.Text.StartsWith("$1") And TxtMain.Text.EndsWith("$") Then
                TxtMain.Text = HW_D1Decrypt(TxtMain.Text)
            ElseIf TxtMain.Text.StartsWith("$2") And TxtMain.Text.EndsWith("$") Then
                TxtMain.Text = HW_D2Decrypt(TxtMain.Text)
            ElseIf TxtMain.Text.StartsWith("$3") And TxtMain.Text.EndsWith("$") Then
                Dim _loc_10 As String = TxtMain.Text
                _loc_10 = _loc_10.Substring(2, _loc_10.Length - 3).Replace("&quot;", """").Replace("&amp;", "&").Replace("&apos;", "'").Replace("&lt;", "<").Replace("&gt;", ">")
                Dim _loc_1 As Integer = Math.Floor(_loc_10.Length / 20)
                Dim _loc_2 As New List(Of String)
                For _loc_3 = 0 To _loc_1 - 1
                    _loc_2.Add(_loc_10.Substring(20 * _loc_3, 20))
                Next
                Dim _loc_5 As Byte() = New Byte(_loc_2.Count * 16 - 1) {}
                For _loc_4 As Integer = 0 To _loc_2.Count - 1
                    Array.Copy(HW_StrToBytes(_loc_2(_loc_4)), 0, _loc_5, _loc_4 * 16, 16)
                Next
                Dim D3Magic As Integer = BitConverter.ToInt32(_loc_5, 52)
                If Not D3Magic = 20220323 OrElse Not HW_D4SetPassword(If(param1.Length > 1, param1, D4KeyStoreLast)) Then
                    HW_D4Reset()
                    Return False
                End If
                Dim D3Checksum As Boolean = _loc_5(14) <> 0
                Dim D3IV As Byte() = New Byte(15) {}
                Array.Copy(_loc_5, 16, D3IV, 0, D3IV.Length)
                Dim D3Salt As Byte() = New Byte(15) {}
                Array.Copy(_loc_5, 32, D3Salt, 0, D3Salt.Length)
                Dim DomainID As Integer = _loc_5(1) * 256 + _loc_5(0)
                Dim KeyID As Integer = _loc_5(3) * 256 + _loc_5(2)
                Dim D3Password As Byte() = New Byte() {}
                D3Password = KMCv2_GetMasterKey(D4KeyStoreFile, DomainID, KeyID)
                If D3Password.Length = 0 Then
                    D4Password = New Byte() {}
                    HW_D4SetPassword()
                    D3Password = D4Password
                End If
                If D3Password.Length = 0 Then
                    HW_D4Reset()
                    Return False
                End If
                Dim D3Key As Byte() = PKCS5_PBKDF2_HMAC(D3Password, D3Salt, BitConverter.ToInt32(_loc_5, 48), 32)
                Dim _loc_6 As Integer = _loc_2.Count - 4 - If(D3Checksum, 2, 0)
                Dim _loc_7 As Byte() = New Byte(_loc_2.Count * 16 - 1) {}
                Dim _loc_8 As Byte() = New Byte(_loc_6 * 16 - 1) {}
                Array.Copy(_loc_5, 64, _loc_8, 0, _loc_6 * 16)
                TxtMain.Text = Encoding.UTF8.GetString(DecryptAES(_loc_8, D3Key, D3IV))
                HW_D4Reset()
            ElseIf TxtMain.Text.StartsWith("$4") And TxtMain.Text.EndsWith("$") Then
                If HW_D4SetPassword(If(param1.Length > 1, param1, D4KeyStoreLast)) Then
                    Dim D4Result As String = HW_D4DecryptString(TxtMain.Text)
                    If D4Result.Length > 0 Then
                        TxtMain.Text = D4Result
                        RadD4.Checked = True
                        HW_D4Reset()
                        Return True
                    End If
                End If
                HW_D4Reset()
                Return False
            Else
                If TxtMain.Text.Length > 0 Then
                    If RadD4.Checked Then
                        Dim D4Result As String = HW_D4EncryptString(TxtMain.Text)
                        If D4Result.Length > 3 Then
                            TxtMain.Text = D4Result
                            Return True
                        End If
                        Return False
                    Else
                        If BoardInfoVersion = 5 Or BoardInfoVersion = 6 Then
                            TxtMain.Text = HW_D2Encrypt(TxtMain.Text)
                        Else
                            If RadD1.Checked Then
                                TxtMain.Text = HW_D1Encrypt(TxtMain.Text, True)
                            Else
                                TxtMain.Text = HW_D2Encrypt(TxtMain.Text, True)
                            End If
                        End If
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            HW_D4Reset()
            Return False
        End Try
    End Function

    Private Function PKCS5_PBKDF2_HMAC(password As Byte(), salt As Byte(), iteration_count As Integer, key_length As Integer) As Byte()
        Using _loc_1 As New Rfc2898DeriveBytes(password, salt, iteration_count, HashAlgorithmName.SHA256)
            Return _loc_1.GetBytes(key_length)
        End Using
    End Function

    Private Sub RadD1_CheckedChanged(sender As Object, e As EventArgs) Handles RadD1.CheckedChanged
        Try
            If RadD1.Checked Then
                BoardInfoVersion = 3
                HW_D4Reset()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RadV3_CheckedChanged(sender As Object, e As EventArgs) Handles RadV3.CheckedChanged
        Try
            If RadV3.Checked Then
                BoardInfoVersion = 3
                HW_D4Reset()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RadV5_CheckedChanged(sender As Object, e As EventArgs) Handles RadV5.CheckedChanged
        Try
            If RadV5.Checked Then BoardInfoVersion = 5
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RadV6_CheckedChanged(sender As Object, e As EventArgs) Handles RadV6.CheckedChanged
        Try
            If RadV6.Checked Then BoardInfoVersion = 6
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RenameConfig(param1 As String)
        Dim _loc_1 As Integer = 0
        Dim _loc_2 As String = param1
        While My.Computer.FileSystem.FileExists(_loc_2)
            _loc_1 += 1
            _loc_2 = Path.GetDirectoryName(param1) & "\" & Path.GetFileNameWithoutExtension(param1) & "_" & _loc_1.ToString().PadLeft(4, "0") & Path.GetExtension(param1)
        End While
        My.Computer.FileSystem.MoveFile(param1, _loc_2)
    End Sub

    Public Function UnGZip(param1 As Byte(), Optional param2 As Boolean = True) As Byte()
        Dim _loc_1 As Byte() = param1
        Using _loc_2 As New MemoryStream()
            Dim _loc_3 As Integer = 0
            If param2 Then
                _loc_3 = BitConverter.ToInt32(_loc_1, _loc_1.Length - 4)
                _loc_2.Write(_loc_1, 0, _loc_1.Length - 4)
            Else
                _loc_3 = BitConverter.ToInt32(_loc_1, 0)
                _loc_2.Write(_loc_1, 4, _loc_1.Length - 4)
            End If
            Dim _loc_4 As Byte() = New Byte(_loc_3 - 1) {}
            _loc_2.Position = 0
            Using _loc_5 As New Compression.GZipStream(_loc_2, CompressionMode.Decompress)
                _loc_5.Read(_loc_4, 0, _loc_4.Length)
            End Using
            Return _loc_4
        End Using
    End Function

End Class
