# coding=utf-8
# A simple demonstration of how to load a QGIS project and then
# show it in a widget.
# This code is public domain, use if for any purpose you see fit.
# Tim Sutton 2015
import os
from qgis.core import QgsProject
from qgis.gui import QgsMapCanvas, QgsLayerTreeMapCanvasBridge
from qgis.core.contextmanagers import qgisapp
from PyQt4.QtCore import QFileInfo, QSize
from PyQt4.QtGui import QWidget, QVBoxLayout
with qgisapp():
    #project_path = os.path.dirname(__file__) + os.path.sep + 'i:/loadqgisprojectPY/test.qgs'
    project_path = 'i:/loadqgisprojectPY/test.qgs'
    print(project_path)
    widget = QWidget()
    canvas = QgsMapCanvas(None)  # will reparent it to widget via layout
    widget.resize(QSize(400, 400))
    layout = QVBoxLayout(widget)
    layout.addWidget(canvas)
    # Load our project
    bridge = QgsLayerTreeMapCanvasBridge(
        QgsProject.instance().layerTreeRoot(), canvas)
    QgsProject.instance().read(QFileInfo(project_path))
    widget.show()