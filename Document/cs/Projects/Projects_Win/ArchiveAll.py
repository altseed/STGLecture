# coding: shift_jis
import os
import zipfile
import shutil

srcDir = "./"
dstDir = "./Archive"
whitelist = [".txt", ".cs", ".sln", ".csproj"]

class section_of_lecture:
    def __init__(self, name):
        self.name = name
        self.projPath = "{0}/{1}".format(srcDir, self.name)
        self.zipPath = "{0}/{1}.zip".format(dstDir, self.name)
    def remove(self):
        if os.path.exists(self.zipPath):
            os.remove(self.zipPath)
    def archive(self):
        zip = zipfile.ZipFile(self.zipPath, "w")
        for dirPath, dirNames, fileNames in os.walk(self.projPath):
            if dirPath.count("obj") > 0:
                continue
            for fn in fileNames:
                path, ext = os.path.splitext(fn)
                if ext in whitelist or fn == "App.config":
                    srcPath = os.path.join(dirPath, fn)
                    arcname = os.path.relpath(srcPath, self.projPath)
                    zip.write(srcPath, arcname.encode("cp932").decode("cp932"))
        zip.close()

def moveToWorkingDir():
    os.chdir(os.path.dirname(__file__))

# この関数の値などを書き換えて圧縮対象を決められます
def getSectionNames():
    indexes = [x for x in range(2, 20)] + [21]
    sectionNames = ["STG{0:0>2}".format(i) for i in indexes]
    sectionNames[len(sectionNames):] = ["STG02_Start"]
    return sectionNames
        
if __name__ == "__main__":
    moveToWorkingDir()
    if not os.path.exists(dstDir):
        os.mkdir(dstDir)
    sectionNames = getSectionNames()
    for name in sectionNames:
        print("Archiving {0}...".format(name))
        section = section_of_lecture(name)
        section.remove()
        section.archive()