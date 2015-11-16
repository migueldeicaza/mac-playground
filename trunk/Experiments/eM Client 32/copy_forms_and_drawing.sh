export LIB="$TARGET_BUILD_DIR/$CONTENTS_FOLDER_PATH/lib"
export DLL="$LIB/mono/4.5"
export GAC="$LIB/mono/gac"
export APP="$LIB/emclient"

cp "$APP/System.Windows.Forms.dll" "$GAC/System.Windows.Forms/4.0.0.0__b77a5c561934e089/System.Windows.Forms.dll"
cp "$APP/System.Windows.Forms.dll.mdb" "$GAC/System.Windows.Forms/4.0.0.0__b77a5c561934e089/System.Windows.Forms.dll.mdb"

cp "$APP/System.Drawing.dll" "$GAC/System.Drawing/4.0.0.0__b03f5f7f11d50a3a/System.Drawing.dll"
cp "$APP/System.Drawing.dll.mdb" "$GAC/System.Drawing/4.0.0.0__b03f5f7f11d50a3a/System.Drawing.dll.mdb"
cp "$APP/System.Drawing.dll" "$LIB/System.Drawing.dll" 2> /dev/null
cp "$APP/System.Drawing.dll.mdb" "$LIB/System.Drawing.dll.mdb"2> /dev/null

cp "$APP/MonoMac.dll" "$LIB/MonoMac.dll"
cp "$APP/MacBridge.dll" "$LIB/MacBridge.dll"
cp "$APP/MailClient.Collections.dll" "$LIB/MailClient.Collections.dll"