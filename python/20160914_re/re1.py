import re

str = "123020-89225300abc"
regstr = r"0\d{2}-\d{8}"

#print re.search(regstr, str).span()
myre = re.compile(regstr)
mylist = re.findall(myre, str)
#print myre.pattern
x = 0
for mystr in mylist:
    print mystr