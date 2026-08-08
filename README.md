# HW Dollar4

华为ONT配置文件实用工具

![HW Patrina preview](https://i.ibb.co/jvSPrVbC/HW-Dollar4-4-20-0.jpg)


# 软件功能

- 支持华为光终端$1、$2、$3、$4方式字符串解密。

- 支持华为光终端$1、$2、$4方式字符串加密。

- 支持使用$2、$4方式加密、解密hw_boardinfo文件，并生成哈希校验文件。

- 支持使用$4方式加密华为光终端XML格式配置文件，并生成哈希校验文件。

- 支持使用默认AES密钥、$2动态密钥和$4方式解密华为光终端XML格式配置文件。

- 支持使用KSF(v2)文件自动搜索UnVisible(一机一密)密钥。


# 系统要求

- Windows 10 及以上版本 64 位操作系统。

- Microsoft .NET Framework 4.7.2 及以上版本。


# 软件限制

⚠️ 用户需自主提供$3、$4方式数据解密UnVisible(一机一密)密钥。

⚠️ 不支持$3方式数据加密。

⚠️ 不支持软件内文本搜索。


# 开源代码

HW Dollar4 使用以下开源组件，其源代码可通过对应的项目页面获取。

aescrypt2 (1.0): https://packetstorm.news/files/id/35655

Mbed TLS (3.6.0 LTS): https://github.com/Mbed-TLS/mbedtls
