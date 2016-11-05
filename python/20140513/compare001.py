# -*- coding: cp936 -*-
#提示用户进行输入
integer1 = raw_input("请输入一个整数:")
integer1 = int(integer1)
integer2 = raw_input("请再次输入一个整数:")
integer2 = int(integer2)

if integer1 < integer2:
    print '%d < %d' %(integer1,integer2)
else:
    print '%d >= %d' %(integer1,integer2)
