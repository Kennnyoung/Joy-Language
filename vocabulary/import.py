import csv

print("hello")

fp = open("./word.txt")
item = fp.read().split('\n')

tt = [1, 2, 3]
print(tt.pop(2))

target = []
for word in item:
	detail = word.split(' ')
	# remove the . in the index
	detail[0] = detail[0].replace(".", "")
	# detail.append("")
	# detail.append(len(detail[1]))
	

	if len(detail) != 4:
		all = ""
		new_detal = [detail[0]]
		for i in range(1, len(detail) - 1):
			# print(detail[i], detail[i].find('.'))
			if detail[i].find('.') == -1:
				all = all + " " + detail[i]
			else:
				new_detal.append(detail[i])
			
			# print(type(detail[i]))
		new_detal.append(detail[len(detail)-1])
		new_detal.insert(1, all)
		new_detal.append("")
		new_detal.append(len(new_detal[1]))
		target.append(new_detal)
	else:
		detail.append("")
		detail.append(len(detail[1]))
		target.append(detail)
	
for x in target:
	print(x)

with open('word.csv', 'w') as csvFile:
    writer = csv.writer(csvFile)
    writer.writerows(target)

csvFile.close()