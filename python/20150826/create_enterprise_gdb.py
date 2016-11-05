# -*- coding: gbk -*-
"""
Name: create_enterprise_gdb.py
Description: Provide connection information to a DBMS instance and create an enterprise geodatabase.
Type  create_enterprise_gdb.py -h or create_enterprise_gdb.py --help for usage
Author: Esri
"""

# Import system modules
import arcpy, os, optparse, sys

# 定义用法和版本
parser = optparse.OptionParser(usage = "usage: %prog [Options]", version="%prog 1.0 for " + arcpy.GetInstallInfo()['Version'] )

# 定义帮助信息和参数选项
parser.add_option ("--DBMS", dest="Database_type", type="choice", choices=['SQLSERVER', 'ORACLE', 'POSTGRESQL', ''], default="", help=u"指定DBMS类型：SQLSERVER，ORACLE，或 POSTGRESQL")                   
parser.add_option ("-i", dest="Instance", type="string", default="", help=u"SQLSERVER实例名，ORACLE网络服务名，或PostgreSQL服务器名")
parser.add_option ("-D", dest="Database", type="string", default="none", help=u"数据库名称，ORACLE不要求")
parser.add_option ("--auth", dest="Account_authentication", type ="choice", choices=['DATABASE_AUTH', 'OPERATING_SYSTEM_AUTH'], default='DATABASE_AUTH', help=u"数据库授权类型：DATABASE_AUTH(数据库身份验证)，OPERATING_SYSTEM_AUTH(操作系统身份验证)。  默认=DATABASE_AUTH")
parser.add_option ("-U", dest="Dbms_admin", type="string", default="", help=u"数据库管理员用户，[Oracle:sys,SQLSERVER:sa,PostgreSQL,超级用户]")
parser.add_option ("-P", dest="Dbms_admin_pwd", type="string", default="", help=u"数据库管理员密码")
parser.add_option ("--schema", dest="Schema_type", type="choice", choices=['SDE_SCHEMA', 'DBO_SCHEMA'], default="SDE_SCHEMA", help=u"只与 SQL Server 相关，并指示地理数据库是在名为 sde 的用户方案中创建还是在数据库的 dbo 方案中创建。 Default=SDE_SCHEMA")
parser.add_option ("-u", dest="Gdb_admin", type="string", default="", help=u"地理数据库管理员用户")
parser.add_option ("-p", dest="Gdb_admin_pwd", type="string", default="", help=u"地理数据库管理员密码")
parser.add_option ("-t", dest="Tablespace", type="string", default="", help=u"表空间名称，只对Oracle或PostgreSQL有效")
parser.add_option ("-l", dest="Authorization_file", type="string", default="", help=u"提供授权企业级 ArcGIS for Server 时创建的密钥代码文件的路径和文件名。")

# 检查输入参数
try:
	(options, args) = parser.parse_args()
	
	#检查是否未输入参数
	if len(sys.argv) == 1:
		print "%s: 错误: %s\n" % (sys.argv[0], "未提供命令参数")
		parser.print_help()
		sys.exit(3)

	#空间数据库连接参数
	database_type = options.Database_type.upper()
	instance = options.Instance
	database = options.Database.lower()	
	account_authentication = options.Account_authentication.upper()
	dbms_admin = options.Dbms_admin
	dbms_admin_pwd = options.Dbms_admin_pwd
	schema_type = options.Schema_type.upper()
	gdb_admin = options.Gdb_admin
	gdb_admin_pwd = options.Gdb_admin_pwd	
	tablespace = options.Tablespace
	license = options.Authorization_file
	
	
	if (database_type == "SQLSERVER"):
		database_type = "SQL_SERVER"
	
	if( database_type ==""):	
		print " \n%s: 错误: \n%s\n" % (sys.argv[0], "必须指定DBMS类型。 (--DBMS)")
		parser.print_help()
		sys.exit(3)		
		
	if (license == ""):
		print " \n%s: 错误: \n%s\n" % (sys.argv[0], "必须指定授权文件。(-l)")
		parser.print_help()
		sys.exit(3)			
	
	if(database_type == "SQL_SERVER"):
		if(schema_type == "SDE_SCHEMA" and gdb_admin.lower() != "sde"):
			print "\n%s: 错误: %s\n" % (sys.argv[0], "在SQL Server上创建SDE方案，地理数据库管理员用户必须是SDE。")
			sys.exit(3)
		if (schema_type == "DBO_SCHEMA" and gdb_admin != ""):
			print "\n警告: %s\n" % ("创建DBO方案，忽略地理数据库管理员用户参数...")
		if( account_authentication == "DATABASE_AUTH" and dbms_admin == ""):
			print "\n%s: 错误: %s\n" % (sys.argv[0], "采用数据库身份验证必须指定数据库管理员用户。")
			sys.exit(3)
		if( account_authentication == "OPERATING_SYSTEM_AUTH" and dbms_admin != ""):
			print "\n警告: %s\n" % ("采用操作系统身份验证，忽略数据库管理员用户参数...")	
	else:
		if (schema_type == "DBO_SCHEMA"):
			print "\n警告: %s %s\n" % (database_type, "只支持SDE方案，自动切换为SDE方案..." )
			
		if( gdb_admin.lower() == ""):
			print "\n%s: 错误: %s\n" % (sys.argv[0], "必须指定地理数据库管理员用户")
			sys.exit(3)

		if( gdb_admin.lower() != "sde"):
			if (database_type == "ORACLE"):
				print "\n数据库管理员用户不是SDE，因此在ORACLE上创建用户方案...\n"
				sys.exit(3)
			else:
				print "\n%s: 错误: %s %s %s.\n" % (sys.argv[0], "数据库类型是", database_type,"地理数据库管理员用户必须是SDE。")
				sys.exit(3)
			
		if( dbms_admin == ""):
			print "\n%s: 错误: %s\n" % (sys.argv[0], "必须指定数据库管理员用户！")
			sys.exit(3)

		if (account_authentication == "OPERATING_SYSTEM_AUTH"):
			print "警告: %s %s\n" % (database_type, "只支持数据库身份验证，自动切换为数据库身份验证..." )

	#获取当前产品授权
	product_license=arcpy.ProductInfo()
	
	
	#检查产品授权级别
	if product_license.upper() == "ARCVIEW" or product_license.upper() == 'ENGINE':
		print "\n找到 " + product_license + " 许可！" + " 创建地理数据库的计算机上必须安装有 ArcGIS for Desktop（标准版或高级版）、具有 Geodatabase Update 扩展模块的 ArcGIS Engine Runtime 或者 ArcGIS for Server（标准版或高级版）。"
		arcpy.AddMessage("+++++++++")
	
	
	try:
		print "正在创建企业级地理数据库...\n"
		arcpy.CreateEnterpriseGeodatabase_management(database_platform=database_type,instance_name=instance, database_name=database, account_authentication=account_authentication, database_admin=dbms_admin, database_admin_password=dbms_admin_pwd, sde_schema=schema_type, gdb_admin_name=gdb_admin, gdb_admin_password=gdb_admin_pwd, tablespace_name=tablespace, authorization_file=license)
		for i in range(arcpy.GetMessageCount()):
			arcpy.Adssage("+++++++++\n")
	except:
		for i in range(arcpy.GetMessageCount()):
			arcpy.AddReturnMessage(i)
			
#未输入选项参数异常处理	
except SystemExit as e:
	if e.code == 2:
		parser.usage = ""
		print "\n"
		parser.print_help()   
		parser.exit(2)
