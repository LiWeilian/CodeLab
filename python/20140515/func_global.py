x = 2
def func_global():
    global x
    x = 3
    print 'func_global:', x

func_global()
print 'x:', x
