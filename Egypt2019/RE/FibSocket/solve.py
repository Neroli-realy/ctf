import struct
import socket
import os

others=[3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181, 6765, 10946, 17711, 28657, 46368, 75025, 121393, 196418, 317811, 514229, 832040, 1346269, 2178309, 3524578, 5702887, 9227465]
for i in range(0,255):
	print(i)
	s=socket.socket(socket.AF_INET,socket.SOCK_STREAM)
	s.connect(('127.0.0.1',7777))
	x=s.recv(1024)
	x=s.recv(1024)
	x =s.recv(4)
	x=s.recv(4)
	n=struct.unpack("I",x)[0]
	ind=others.index(n)
	sen=struct.pack("I",others[ind+1])
	s.send(sen)  
	x=s.recv(4) 
	x=s.recv(1024) 
	l =int(input())
	s.send(struct.pack('B',233))
	x=s.recv(1024)  
	try:   
	 while True:
	  x=s.recv(1024) 
	  print(x.decode('utf-8')) 
	 break 
	except: 
		continue