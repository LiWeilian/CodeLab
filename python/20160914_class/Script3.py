class Employee:
    empCount = 0
    
    def __init__(self, name, salary):
        self.name = name
        self.salary = salary
        Employee.empCount += 1

    def __del__(self):
        class_name = self.__class__.__name__
        print class_name, "销毁"

    def displayCount(self):
        print "Total Employee %d" % Employee.empCount

    def displayEmployee(self):
        print "Name : ", self.name, ", Salary : ", self.salary

emp1 = Employee("Zara", 2000)
emp1.displayCount()
emp1.displayEmployee()

emp2 = Employee("Manni", 5000)
emp2.displayCount()
emp2.displayEmployee()

del emp1
del emp2