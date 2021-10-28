import numpy as np



def printit(pat, name, x):
    print(name, len(pat), len(x))
    res = ""
    for e in x:
        res += f"{e}, "
    print(res)
    input()


if __name__ == "__main__":
  onlinepatients = np.array([0,1,10,1000,10000,116,14,20,20,3,3,3,3,3,3,3,4,4,4,4,4,4,45,46,5,5,50,50,5000,50000,6,60,69,7,7,7,8,96,96,96,96])
  greedy = np.array([10.571372600039467, 8.115956499939784, 6.556747599970549, 10.931732599972747, 7.11249269999098, 8.131322300061584, 5.932899000006728, 11.088590599945746, 13.137539300019853, 9.938003799994476, 12.716602699947543, 8.510359999956563, 7.345460699987598, 6.814750100020319, 8.515942999976687, 6.139063199982047, 6.314132899977267, 12.003123100032099, 10.560479099978693, 7.64918549999129, 12.673920499975793, 8.700026800041087, None, None, None, 7.107037200010382, 7.061122999992222, 21.436176299932413, 13.582797699957155, 8.51964680000674, 12.773992899921723, 8.49220650002826, 18.78787060000468, 12.070060399943031, 10.81425469997339, 6.948295700014569, 24.98403460008558, 6.101571899955161, 6.987599699990824, 7.099926199996844, 6.927796199917793])
  forward = np.array([None, 14.292542599956505, 6.636926500010304, 12.554151299991645, 35.914105299976654, 10.155631799949333, 10.933927499921992, 14.15910100005567, 6.621171100065112, 6.681783900014125, 7.562370500061661, 8.777973299962468, 9.102573300013319, 14.372376599931158, 18.5097800999647, 12.200186799978837, 10.198061400093138, 7.082173600094393, 5.760237299953587, 13.843799899914302, 7.4048649000469595, 6.293858299963176, 15.53468070004601, 8.413077999954112, 8.070473699946888, 8.204129299963824, 6.0438820999115705, 18.77217719994951, 7.4732927000150084, 8.004679100005887, 10.51502559997607, 6.73784129996784, 11.319442300009541, 6.389631300000474, 6.785726599977352, 6.9827869000146165, 7.530977699905634, 13.005853099981323, 6.606014300021343, 6.71105979999993, 6.694386500050314])
  verygreedy = np.array([6.391206200001761, 6.654318399960175, 6.007540000020526, 6.521252000005916, 5.728511200053617, 5.681826599990018, 6.318568599992432, 10.82954189996235, 5.8537527999142185, 9.830818000016734, 6.2043613999849185, 6.322921699960716, 47.126733700046316, 5.56366809993051, 6.374780799960718, 6.883513899985701, 5.698787000030279, 6.426950299995951, 7.027203899924643, 6.550271300016902, 8.1938203999307, 5.826058700098656, 7.03566289995797, 6.414267300046049, 32.72031160001643, 20.51022609998472, 8.835814099991694, 6.335863700020127, 6.9600720999296755, 7.1974528999999166, 8.104976100032218, 10.20721139991656, 7.1389743000036106, 17.9559856998967, 6.596036000060849, 8.302230200031772, 6.177806199993938, 11.160774199990556, 6.245331700076349, 6.3443064000457525, 7.922323300037533])

  oppatients = onlinepatients[onlinepatients <= 15]
  opgreedy = greedy[onlinepatients <= 15]
  opforward = forward[onlinepatients <= 15]
  opverygreedy = verygreedy[onlinepatients <= 15]

  printit(oppatients, "onlinepatients", oppatients)
  printit(oppatients, "opgreedy", opgreedy)
  printit(oppatients, "opforward", opforward)
  printit(oppatients, "opverygreedy",opverygreedy)



    