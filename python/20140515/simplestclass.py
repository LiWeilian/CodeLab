class Person:
    def __init__(self, name):
        self.name = name
    def sayHi(self):
        print 'Hi! My name is',self.name

p = Person('LiWeilian')
p.sayHi()
