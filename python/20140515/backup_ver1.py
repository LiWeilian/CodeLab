#Windows Edition
import os
import time

source = [r'F:\WorkFiles-TempFiles\20140515_testPythonBackup\target1', r'F:\WorkFiles-TempFiles\20140515_testPythonBackup\target2']
target_dir = r'F:\WorkFiles-TempFiles\20140515_testPythonBackup\backup'

target = target_dir + '\\' + time.strftime('%Y%m%d%H%M%S') + '.zip'

#UNIX Edition
#zip_command = "zip -qr '%s' %s" % (target, ' '.join(source))
#Windows Edition
print 'source:', ' '.join(source)
print target
zip_command = "rar a %s %s" % (target, ' '.join(source))

print 'zip_command:',zip_command
if os.system(zip_command) == 0:
    print 'Successful backup to', target
else:
    print 'Backup FAILED'
