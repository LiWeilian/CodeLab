import sys
from sys import argv
from sys import *

print 'The command line arguments length is:',len(sys.argv)
print 'The command line arguments are:'
for i in argv:
    print i
print '\n\nThe PYTHONPATH is', sys.path
