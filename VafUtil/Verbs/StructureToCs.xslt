<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
								xmlns:msxsl="urn:schemas-microsoft-com:xslt"
								xmlns:vafutil="urn:VafUtil"
								exclude-result-prefixes="msxsl">
	<xsl:output method="text" />

	<xsl:param name="kind" />
	<xsl:param name="namespace" />
	<xsl:param name="views" />
	<xsl:param name="verbose" />
	<xsl:param name="aliasclass">Alias</xsl:param>
	<xsl:param name="configurationclass">Configuration</xsl:param>
	<xsl:param name="viewprefix">VW.</xsl:param>
	<xsl:param name="ownerpropertydefprefix">PD.Owner.</xsl:param>

	<xsl:key name="objtype" match="/archive/structure/objtypes/objtype" use="number(@id)" />

	<xsl:template name="AliasFile">
		<xsl:text xml:space="preserve">using System;

namespace </xsl:text>
		<xsl:value-of select="$namespace" />
		<xsl:text xml:space="preserve"> {
	public static class </xsl:text>
		<xsl:value-of select="$aliasclass"/>
		<xsl:text xml:space="preserve"> {</xsl:text>
		<xsl:apply-templates select="*/*[normalize-space(@aliases) and not(@deleted='true') and (not(self::view) or $views)]" mode="AliasFile">
			<xsl:sort select="count(parent::*/following-sibling::*)" data-type="number" order="descending" />
			<xsl:sort select="@aliases" />
		</xsl:apply-templates>
		<xsl:text xml:space="preserve">  }
}</xsl:text>
	</xsl:template>

	<xsl:template match="*" mode="AliasFile">
		<xsl:param name="aliases" select="concat(normalize-space(@aliases), ';')" />
		<xsl:variable name="rawalias" select="substring-before($aliases, ';')"/>
		<xsl:variable name="alias">
			<xsl:if test="$rawalias">
				<xsl:if test="self::view">
					<xsl:value-of select="$viewprefix"/>
				</xsl:if>
				<xsl:value-of select="$rawalias"/>
			</xsl:if>
		</xsl:variable>
		<xsl:if test="$alias!=''">
			<xsl:if test="$verbose">
				<xsl:message xml:space="preserve">Alias for <xsl:value-of select="substring-before(concat(local-name(.), '.'), '.')"/> <xsl:value-of select="$alias"/></xsl:message>
			</xsl:if>
			<xsl:text xml:space="preserve">
		public const string </xsl:text>
			<xsl:value-of select="vafutil:NormalizeName($alias)" />
			<xsl:text xml:space="preserve"> = </xsl:text>
			<xsl:value-of select="vafutil:CSharpString($rawalias, 4)" />
			<xsl:text xml:space="preserve">;
</xsl:text>
		</xsl:if>
		<xsl:variable name="rest" select="substring-after($aliases, ';')" />
		<xsl:if test="$rest">
			<xsl:apply-templates mode="AliasFile">
				<xsl:with-param name="aliases" select="$rest" />
			</xsl:apply-templates>
		</xsl:if>
	</xsl:template>

	<xsl:template name="ConfigurationFile">
		<xsl:text xml:space="preserve">using System;
using System.Linq;

using MFiles.VAF.Configuration;

using MFilesAPI;

namespace </xsl:text>
		<xsl:value-of select="$namespace" />
		<xsl:text xml:space="preserve"> {
	public partial class </xsl:text>
		<xsl:value-of select="$configurationclass"/>
		<xsl:text xml:space="preserve"> {</xsl:text>
		<xsl:apply-templates select="*/*[(normalize-space(@aliases) or self::propertydef/datatype/Lookup/@isowner='true') and not(@deleted='true') and (not(self::view) or $views)]" mode="ConfigurationFile">
			<xsl:sort select="count(parent::*/following-sibling::*)" data-type="number" order="descending" />
			<xsl:sort select="normalize-space(concat(@aliases, ' ', $ownerpropertydefprefix, self::propertydef/datatype/Lookup[@isowner='true']/@name))" />
		</xsl:apply-templates>
		<xsl:text xml:space="preserve">
		internal void InitializeOwnerPropertyDefs(Vault vault) {
			var objectTypeOwners = vault.ValueListOperations
					.GetValueLists()
					.Cast&lt;ObjType>()
					.ToDictionary(ot => ot.ID, ot => ot.OwnerPropertyDef);</xsl:text>
		<xsl:for-each select="//self::propertydef[datatype/Lookup/@isowner='true']">
			<xsl:sort select="datatype/Lookup/@name" />
			<xsl:variable name="objtype" select="key('objtype', number(datatype/Lookup/@otid))" />
			<xsl:if test="normalize-space($objtype/@aliases)">
				<xsl:text xml:space="preserve">
			</xsl:text>
				<xsl:value-of select="vafutil:NormalizeName(concat($ownerpropertydefprefix, datatype/Lookup/@name))" />
				<xsl:text xml:space="preserve">.Set(objectTypeOwners[</xsl:text>
				<xsl:value-of select="vafutil:NormalizeName(substring-before(concat($objtype/@aliases, ';'), ';'))" />
				<xsl:text xml:space="preserve">]);</xsl:text>
			</xsl:if>
		</xsl:for-each>
		<xsl:text xml:space="preserve">
		}
	}
}
</xsl:text>
	</xsl:template>

	<xsl:template match="propertydef[datatype/Lookup/@isowner='true']" mode="ConfigurationFile">
		<xsl:variable name="objtype" select="key('objtype', number(datatype/Lookup/@otid))" />
		<xsl:if test="normalize-space($objtype/@aliases)">
			<xsl:text xml:space="preserve">
		[MFPropertyDef(Datatypes = new[] { MFDataType.MFDatatypeLookup }, AllowEmpty = true)]
		public readonly MFIdentifier </xsl:text>
			<xsl:value-of select="vafutil:NormalizeName(concat($ownerpropertydefprefix, datatype/Lookup/@name))" />
			<xsl:text xml:space="preserve"> = new MFIdentifier();
</xsl:text>
		</xsl:if>
	</xsl:template>

	<xsl:template match="*" mode="ConfigurationFile">
		<xsl:param name="aliases" select="concat(normalize-space(@aliases), ';')" />
		<xsl:variable name="alias">
			<xsl:if test="substring-before($aliases, ';')">
				<xsl:if test="self::view">
					<xsl:value-of select="$viewprefix"/>
				</xsl:if>
				<xsl:value-of select="substring-before($aliases, ';')"/>
			</xsl:if>
		</xsl:variable>
		<xsl:if test="$alias!=''">
			<xsl:if test="$verbose">
				<xsl:message xml:space="preserve">MFIdentifier for <xsl:value-of select="substring-before(concat(local-name(.), '.'), '.')"/> <xsl:value-of select="$alias"/></xsl:message>
			</xsl:if>
			<xsl:text xml:space="preserve">
		[</xsl:text>
			<xsl:choose>
				<xsl:when test="self::class">
					<xsl:text>MFClass</xsl:text>
				</xsl:when>
				<xsl:when test="self::classgroup">
					<xsl:text>MFClassGroup</xsl:text>
				</xsl:when>
				<xsl:when test="self::namedacl">
					<xsl:text>MFNamedACL</xsl:text>
				</xsl:when>
				<xsl:when test="self::objtype[@realobj='false']">
					<xsl:text>MFValueList</xsl:text>
				</xsl:when>
				<xsl:when test="self::objtype[@realobj='true']">
					<xsl:text>MFObjType</xsl:text>
				</xsl:when>
				<xsl:when test="self::propertydef">
					<xsl:text xml:space="preserve">MFPropertyDef(Datatypes = new[] { MFDataType.</xsl:text>
					<xsl:choose>
						<xsl:when test="datatype/Text">MFDatatypeText</xsl:when>
						<xsl:when test="datatype/MultiLineText">MFDatatypeMultiLineText</xsl:when>
						<xsl:when test="datatype/Integer">MFDatatypeInteger</xsl:when>
						<xsl:when test="datatype/Integer64">MFDatatypeInteger64</xsl:when>
						<xsl:when test="datatype/Float">MFDatatypeFloating</xsl:when>
						<xsl:when test="datatype/Date">MFDatatypeDate</xsl:when>
						<xsl:when test="datatype/Time">MFDatatypeTime</xsl:when>
						<xsl:when test="datatype/Timestamp">MFDatatypeTimestamp</xsl:when>
						<xsl:when test="datatype/Boolean">MFDatatypeBoolean</xsl:when>
						<xsl:when test="datatype/Lookup">MFDatatypeLookup</xsl:when>
						<xsl:when test="datatype/MultiSelectLookup">MFDatatypeMultiSelectLookup</xsl:when>
						<xsl:otherwise>
							<xsl:message terminate="yes" xml:space="preserve">Unknown property definition data type '<xsl:value-of select="local-name(datatype/*)" />'</xsl:message>
						</xsl:otherwise>
					</xsl:choose>
					<xsl:text xml:space="preserve"> }</xsl:text>
					<xsl:variable name="objtype" select="key('objtype', number(objecttype[@all='false']/@otid))" />
					<xsl:if test="$objtype">
						<xsl:text xml:space="preserve">, ObjectType = </xsl:text>
						<xsl:choose>
							<xsl:when test="objecttype/@otid=-102">MFBuiltInObjectType.MFBuiltInObjectTypeDocumentOrDocumentCollection</xsl:when>
							<xsl:when test="objecttype/@otid=0">MFBuiltInObjectType.MFBuiltInObjectTypeDocument</xsl:when>
							<xsl:when test="objecttype/@otid=9">MFBuiltInObjectType.MFBuiltInObjectTypeDocumentCollection</xsl:when>
							<xsl:when test="objecttype/@otid=10">MFBuiltInObjectType.MFBuiltInObjectTypeAssignment</xsl:when>
							<xsl:when test="objecttype/@otid=20">MFBuiltInObjectType.MFBuiltInObjectTypeEmail</xsl:when>
							<xsl:when test="objecttype/@otid=21">MFBuiltInObjectType.MFBuiltInObjectTypeEmailConversation</xsl:when>
							<xsl:when test="$objtype[normalize-space(@aliases)]">
								<xsl:value-of select="$aliasclass"/>
								<xsl:text>.</xsl:text>
								<xsl:value-of select="vafutil:NormalizeName(substring-before(concat($objtype/@aliases, ';'), ';'))" />
							</xsl:when>
							<xsl:otherwise>
								<xsl:text>"</xsl:text>
								<xsl:value-of select="$objtype/@guid" />
								<xsl:text xml:space="preserve">" /* </xsl:text>
								<xsl:value-of select="$objtype/@name" />
								<xsl:text xml:space="preserve"> */</xsl:text>
							</xsl:otherwise>
						</xsl:choose>
					</xsl:if>
					<xsl:text>)</xsl:text>
				</xsl:when>
				<xsl:when test="self::transition">
					<xsl:text>MFStateTransition</xsl:text>
				</xsl:when>
				<xsl:when test="self::group">
					<xsl:text>MFUserGroup</xsl:text>
				</xsl:when>
				<xsl:when test="self::valuelist">
					<xsl:text>MFValueList</xsl:text>
				</xsl:when>
				<xsl:when test="self::workflow | self::workflow.2">
					<xsl:text>MFWorkflow</xsl:text>
				</xsl:when>
				<xsl:when test="self::state | self::state.2">
					<xsl:text>MFState</xsl:text>
				</xsl:when>
				<xsl:when test="self::view">
					<xsl:text>MFView</xsl:text>
				</xsl:when>
				<xsl:otherwise>
					<xsl:message terminate="yes" xml:space="preserve">Unknown item type '<xsl:value-of select="local-name(.)" />'</xsl:message>
				</xsl:otherwise>
			</xsl:choose>
			<xsl:text xml:space="preserve">]
		public readonly MFIdentifier </xsl:text>
			<xsl:value-of select="vafutil:NormalizeName($alias)" />
			<xsl:text xml:space="preserve"> = </xsl:text>
			<xsl:value-of select="concat($aliasclass, '.', vafutil:NormalizeName($alias))" />
			<xsl:text xml:space="preserve">;
</xsl:text>
		</xsl:if>
		<xsl:variable name="rest" select="substring-after($aliases, ';')" />
		<xsl:if test="$rest">
			<xsl:apply-templates mode="ConfigurationFile">
				<xsl:with-param name="aliases" select="$rest" />
			</xsl:apply-templates>
		</xsl:if>
	</xsl:template>

	<xsl:template match="/archive">
		<xsl:apply-templates select="structure" />  
	</xsl:template>
	
	<xsl:template match="/archive/structure">
		<xsl:choose>
			<xsl:when test="$kind='AliasFile'">
				<xsl:call-template name="AliasFile" />
				<xsl:message xml:space="preserve">Generated alias file.</xsl:message>
			</xsl:when>
			<xsl:when test="$kind='ConfigurationFile'">
				<xsl:call-template name="ConfigurationFile" />
				<xsl:message xml:space="preserve">Generated identifiers file.</xsl:message>
			</xsl:when>
			<xsl:otherwise>
				<xsl:message terminate="yes" xml:space="preserve">Unknown kind '<xsl:value-of select="$kind" />'</xsl:message>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<xsl:template match="@* | node()">
		<xsl:copy>
			<xsl:apply-templates select="@* | node()" />
		</xsl:copy>
	</xsl:template>
</xsl:stylesheet>