class Parent:
    parentAttr = 100
    def __init__(self):
        print "调用父类构造函数"

    def __str__(self):
        return "Class_Name: "

    def parentMethod(self):
        print "调用父类方法"

    def setAttr(self, attr):
        Parent.parentAttr = attr

    def getAttr(self):
        print "父类属性 :", Parent.parentAttr
        
class Child(Parent): # 定义子类
    def __init__(self):
      print "调用子类构造方法"

    def __str__(self):
        return "Sub_Class_Name: "

    def childMethod(self):
      print "调用子类方法 child method"

    def setAttr(self, attr):
        Parent.parentAttr = attr + 100

    def getAttr(self):
        print "子类属性 :", Parent.parentAttr + 100

c = Child()          # 实例化子类
c.childMethod()      # 调用子类的方法
c.parentMethod()     # 调用父类方法
c.setAttr(200)       # 再次调用父类的方法
c.getAttr()          # 再次调用父类的方法
print c