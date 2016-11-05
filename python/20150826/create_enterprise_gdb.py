# -*- coding: gbk -*-
"""
Name: create_enterprise_gdb.py
Description: Provide connection information to a DBMS instance and create an enterprise geodatabase.
Type  create_enterprise_gdb.py -h or create_enterprise_gdb.py --help for usage
Author: Esri
"""

# Import system modules
import arcpy, os, optparse, sys

# �����÷��Ͱ汾
parser = optparse.OptionParser(usage = "usage: %prog [Options]", version="%prog 1.0 for " + arcpy.GetInstallInfo()['Version'] )

# ���������Ϣ�Ͳ���ѡ��
parser.add_option ("--DBMS", dest="Database_type", type="choice", choices=['SQLSERVER', 'ORACLE', 'POSTGRESQL', ''], default="", help=u"ָ��DBMS���ͣ�SQLSERVER��ORACLE���� POSTGRESQL")                   
parser.add_option ("-i", dest="Instance", type="string", default="", help=u"SQLSERVERʵ������ORACLE�������������PostgreSQL��������")
parser.add_option ("-D", dest="Database", type="string", default="none", help=u"���ݿ����ƣ�ORACLE��Ҫ��")
parser.add_option ("--auth", dest="Account_authentication", type ="choice", choices=['DATABASE_AUTH', 'OPERATING_SYSTEM_AUTH'], default='DATABASE_AUTH', help=u"���ݿ���Ȩ���ͣ�DATABASE_AUTH(���ݿ������֤)��OPERATING_SYSTEM_AUTH(����ϵͳ�����֤)��  Ĭ��=DATABASE_AUTH")
parser.add_option ("-U", dest="Dbms_admin", type="string", default="", help=u"���ݿ����Ա�û���[Oracle:sys,SQLSERVER:sa,PostgreSQL,�����û�]")
parser.add_option ("-P", dest="Dbms_admin_pwd", type="string", default="", help=u"���ݿ����Ա����")
parser.add_option ("--schema", dest="Schema_type", type="choice", choices=['SDE_SCHEMA', 'DBO_SCHEMA'], default="SDE_SCHEMA", help=u"ֻ�� SQL Server ��أ���ָʾ�������ݿ�������Ϊ sde ���û������д������������ݿ�� dbo �����д����� Default=SDE_SCHEMA")
parser.add_option ("-u", dest="Gdb_admin", type="string", default="", help=u"�������ݿ����Ա�û�")
parser.add_option ("-p", dest="Gdb_admin_pwd", type="string", default="", help=u"�������ݿ����Ա����")
parser.add_option ("-t", dest="Tablespace", type="string", default="", help=u"��ռ����ƣ�ֻ��Oracle��PostgreSQL��Ч")
parser.add_option ("-l", dest="Authorization_file", type="string", default="", help=u"�ṩ��Ȩ��ҵ�� ArcGIS for Server ʱ��������Կ�����ļ���·�����ļ�����")

# ����������
try:
	(options, args) = parser.parse_args()
	
	#����Ƿ�δ�������
	if len(sys.argv) == 1:
		print "%s: ����: %s\n" % (sys.argv[0], "δ�ṩ�������")
		parser.print_help()
		sys.exit(3)

	#�ռ����ݿ����Ӳ���
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
		print " \n%s: ����: \n%s\n" % (sys.argv[0], "����ָ��DBMS���͡� (--DBMS)")
		parser.print_help()
		sys.exit(3)		
		
	if (license == ""):
		print " \n%s: ����: \n%s\n" % (sys.argv[0], "����ָ����Ȩ�ļ���(-l)")
		parser.print_help()
		sys.exit(3)			
	
	if(database_type == "SQL_SERVER"):
		if(schema_type == "SDE_SCHEMA" and gdb_admin.lower() != "sde"):
			print "\n%s: ����: %s\n" % (sys.argv[0], "��SQL Server�ϴ���SDE�������������ݿ����Ա�û�������SDE��")
			sys.exit(3)
		if (schema_type == "DBO_SCHEMA" and gdb_admin != ""):
			print "\n����: %s\n" % ("����DBO���������Ե������ݿ����Ա�û�����...")
		if( account_authentication == "DATABASE_AUTH" and dbms_admin == ""):
			print "\n%s: ����: %s\n" % (sys.argv[0], "�������ݿ������֤����ָ�����ݿ����Ա�û���")
			sys.exit(3)
		if( account_authentication == "OPERATING_SYSTEM_AUTH" and dbms_admin != ""):
			print "\n����: %s\n" % ("���ò���ϵͳ�����֤���������ݿ����Ա�û�����...")	
	else:
		if (schema_type == "DBO_SCHEMA"):
			print "\n����: %s %s\n" % (database_type, "ֻ֧��SDE�������Զ��л�ΪSDE����..." )
			
		if( gdb_admin.lower() == ""):
			print "\n%s: ����: %s\n" % (sys.argv[0], "����ָ���������ݿ����Ա�û�")
			sys.exit(3)

		if( gdb_admin.lower() != "sde"):
			if (database_type == "ORACLE"):
				print "\n���ݿ����Ա�û�����SDE�������ORACLE�ϴ����û�����...\n"
				sys.exit(3)
			else:
				print "\n%s: ����: %s %s %s.\n" % (sys.argv[0], "���ݿ�������", database_type,"�������ݿ����Ա�û�������SDE��")
				sys.exit(3)
			
		if( dbms_admin == ""):
			print "\n%s: ����: %s\n" % (sys.argv[0], "����ָ�����ݿ����Ա�û���")
			sys.exit(3)

		if (account_authentication == "OPERATING_SYSTEM_AUTH"):
			print "����: %s %s\n" % (database_type, "ֻ֧�����ݿ������֤���Զ��л�Ϊ���ݿ������֤..." )

	#��ȡ��ǰ��Ʒ��Ȩ
	product_license=arcpy.ProductInfo()
	
	
	#����Ʒ��Ȩ����
	if product_license.upper() == "ARCVIEW" or product_license.upper() == 'ENGINE':
		print "\n�ҵ� " + product_license + " ��ɣ�" + " �����������ݿ�ļ�����ϱ��밲װ�� ArcGIS for Desktop����׼���߼��棩������ Geodatabase Update ��չģ��� ArcGIS Engine Runtime ���� ArcGIS for Server����׼���߼��棩��"
		arcpy.AddMessage("+++++++++")
	
	
	try:
		print "���ڴ�����ҵ���������ݿ�...\n"
		arcpy.CreateEnterpriseGeodatabase_management(database_platform=database_type,instance_name=instance, database_name=database, account_authentication=account_authentication, database_admin=dbms_admin, database_admin_password=dbms_admin_pwd, sde_schema=schema_type, gdb_admin_name=gdb_admin, gdb_admin_password=gdb_admin_pwd, tablespace_name=tablespace, authorization_file=license)
		for i in range(arcpy.GetMessageCount()):
			arcpy.Adssage("+++++++++\n")
	except:
		for i in range(arcpy.GetMessageCount()):
			arcpy.AddReturnMessage(i)
			
#δ����ѡ������쳣����	
except SystemExit as e:
	if e.code == 2:
		parser.usage = ""
		print "\n"
		parser.print_help()   
		parser.exit(2)
