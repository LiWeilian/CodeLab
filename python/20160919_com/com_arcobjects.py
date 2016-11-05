import comtypes
from comtypes.client import GetModule
from comtypes.client import CreateObject

def getAoModule(moduleName):
    GetModule('C:\\Program Files (x86)\\ArcGIS\\Desktop10.1\\com' + moduleName)
	
def initAoModule():
	aoLibPath = 'C:\\Program Files (x86)\\ArcGIS\\Desktop10.1\\com\\'
	GetModule(aoLibPath + 'esriSystem.olb')
	GetModule(aoLibPath + 'esriGeometry.olb')
	GetModule(aoLibPath + 'esriGeoDatabase.olb')

def aoObj(myClass, myInterface):
    try:
        obj = CreateObject(myClass, interface=myInterface)
        return obj
    except:
        return None

initAoModule()
import comtypes.gen.esriSystem as esriSystem
import comtypes.gen.esriGeometry as esriGeometry
import comtypes.gen.esriGeoDatabase as esriGeoDatabase

#pt = aoObj(esriGeometry.Point, esriGeometry.IPoint)
