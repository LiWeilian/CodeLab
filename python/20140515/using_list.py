myList = ['3', '4', 1, 2, '5']

for item in myList:
    print item

print 'sort:'
myList.sort()
for item in myList:
    print item

print 'del:'
del myList[3]
for item in myList:
    print item

print 'append:'
myList.append(0)
for item in myList:
    print item

print 'sort:'
myList.sort()
for item in myList:
    print item
