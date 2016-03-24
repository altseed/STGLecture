# coding: shift_jis
import zipfileJp
import shutil
import os

dstDir = "Extract"

class section_of_lecture:
    def __init__(self, name):
        self.name = name
        self.projDir = "{0}/{1}/{1}".format(dstDir, self.name)
    def remove(self):
        if os.path.exists("{0}/{1}/".format(dstDir, self.name)):
            shutil.rmtree(dstDir + "/" + self.name)
    def extractZip(self):
        zip = zipfileJp.ZipFile(self.name + ".zip")
        zip.extractall(dstDir + "/" + self.name)
        zip.close()
    def normalize(self):
        if not os.path.exists(self.projDir):
            extracted = "{0}/{1}/".format(dstDir, self.name)
            os.mkdir(self.projDir)
            shutil.copytree(extracted + "/bin", self.projDir + "/bin")
            shutil.copytree(extracted + "/STG", self.projDir + "/STG")
            shutil.copy(extracted + "/STG.sln", self.projDir + "/STG.sln")
            shutil.rmtree(extracted + "/bin")
            shutil.rmtree(extracted + "/STG")
            os.remove(extracted + "/STG.sln")
    def copyResources(self):
        copyToProj = lambda f : shutil.copy("Script/" + f, self.projDir + "/STG/")
        copyToProj("Altseed.dll")
        copyToProj("Altseed.XML")
        copyToProj("Altseed_core.dll")
        shutil.copytree("Script/Resources", self.projDir + "/bin/Resources/")

def moveToWorkingDir():
    os.chdir(os.path.dirname(__file__))
    os.chdir("..")

# この関数の値などを書き換えて解凍対象を決められます
def getSectionNames():
    indexes = [x for x in range(2, 19)] + [22]
    sectionNames = ["STG{0:0>2}".format(i) for i in indexes]
    sectionNames[len(sectionNames):] = ["STG01_Start", "STG01_End", "STG02_Start"]
    return sectionNames

if __name__ == "__main__":
    moveToWorkingDir()
    if not os.path.exists(dstDir):
        os.mkdir(dstDir)
    sectionNames = getSectionNames()
    for name in sectionNames:
        print("Extracting {0}...".format(name))
        section = section_of_lecture(name)
        section.remove()
        section.extractZip()
        section.normalize()
        section.copyResources()
