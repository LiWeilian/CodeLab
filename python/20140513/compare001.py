# -*- coding: cp936 -*-
#��ʾ�û���������
integer1 = raw_input("������һ������:")
integer1 = int(integer1)
integer2 = raw_input("���ٴ�����һ������:")
integer2 = int(integer2)

if integer1 < integer2:
    print '%d < %d' %(integer1,integer2)
else:
    print '%d >= %d' %(integer1,integer2)
