# -*- coding: gbk -*-
import arcpy, os, optparse, sys

parser = optparse.OptionParser(usage = "usage: %prog [Options]", version="%prog 1.0 for " + arcpy.GetInstallInfo()['Version'])

parser.add_option ("--DBMS", dest="Database_type", type="choice", choices=['SQLSERVER', 'ORACLE', 'POSTGRESQL', ''], default="", help=u"哈")                   
parser.add_option ("-i", dest="Instance", type="string", default="", help="")

try:
    s = "测试"
    print s
    (options, args) = parser.parse_args()

    if len(sys.argv) == 1:
        print "%s: error: %s\n" % (sys.argv[0], "No command options given")
        parser.print_help()
        sys.exit(3)

    print options.Database_type.upper()
    print options.Instance
except SystemExit as e:
    if e.code == 2:
        parser.usage = ""
        print "\n"
        parser.print_help()
        parser.exit(2)
