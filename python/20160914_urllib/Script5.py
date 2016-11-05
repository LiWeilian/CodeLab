import urllib

url = "https://www.baidu.com"
f = urllib.urlopen(url)
print "status code : ", f.getcode()