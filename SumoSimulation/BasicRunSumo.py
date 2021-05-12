# -*- coding: utf-8 -*-
"""
Created on Tue Apr 20 15:04:54 2021

@author: gielo
"""
import sys, getopt
import time
import os
import pandas
import traci  # noqa
import sumolib
from sumolib import checkBinary  # noqa
netfile = r'C:/Users/20210124/Documents/RealDeal/Unity/TestCSCode/grid.net.xml'
cfgfile = r'C:/Users/20210124/Documents/RealDeal/Unity/TestCSCode/grid.sumocfg'
port = 4001
# we need to import python modules from the $SUMO_HOME/tools directory
if 'SUMO_HOME' in os.environ:
    tools = os.path.join(os.environ['SUMO_HOME'], 'tools')
    sys.path.append(tools)
else:
    sys.exit("please declare environment variable 'SUMO_HOME'")

def parseOptions(argv):
    try:
        opts, args = getopt.getopt(argv,"",["netfile=","cfgfile=","port="])
    except getopt.GetoptError:
        print('test.py -i <inputfile> -o <outputfile>')
        sys.exit(2)
    for opt, arg in opts:
        if opt in ("-i", "--netfile"):
            netfile = arg
        elif opt in ("-o", "--cfgfile"):
            cfgfile = arg
        elif opt in ("-p", "--port"):
            port = int(arg)
    print('Net file is ', netfile)
    print('Cfg file is ', cfgfile)
    print('Traci connection on port ', port)
    return netfile, cfgfile, port

def runSimulation():
    for i in range(1000):
        if not (i%10):
            print("step: ", i)
        traci.simulationStep()  
        time.sleep(0.01)
        print("test pyth")
    traci.close()

# this is the main entry point of this script
netfile, cfgfile, port = parseOptions(sys.argv[1:])

net = sumolib.net.readNet(netfile)
sumoBinary = checkBinary('sumo-gui') #'sumo' or 'sumo-gui' (graphical interface)
traci.start([sumoBinary, "-c", cfgfile, '--num-clients', '2'
                    , "--step-length", str(0.02)
                    , "--collision.stoptime", "25"
                    , "--collision.action", "remove"
                    , "--lateral-resolution", "0.4", "--start"
                 ], port=port, traceFile="test.txt")
print("Setting python order")
traci.setOrder(2)
print("Order python set")
#time.sleep(5)
    
runSimulation()

